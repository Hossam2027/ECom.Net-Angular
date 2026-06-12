using ECom.Core.DTO;
using ECom.Core.Entites;
using ECom.Core.Interface;
using ECom.Core.Services;
using ECom.infrastrucrure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECom.infrastrucrure.Repositries.Service
{
    public class AuthServices : IAuth
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IGenToken genToken;
        private readonly AppDbContext _dbContext;
        public AuthServices(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            IConfiguration configuration,
            IGenToken genToken,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            this.genToken = genToken;
            this._dbContext = dbContext;
        }

        // ================= REGISTER =================
        public async Task<object> RegisterAsync(RegDto regDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(regDto.Email);

            if (existingUser != null)
            {
                return new
                {
                    success = false,
                    message = "Email already exists"
                };
            }
            var user = new AppUser
            {
                Name = regDto.Name,
                Email = regDto.Email,
                UserName = regDto.Name
            };

            var result = await _userManager.CreateAsync(user, regDto.Password);

            if (!result.Succeeded)
                return new
                {
                    success = false,
                    message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            // 🔥 Identity token (NO ENCODING)
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


            var confirmationLink =
    $"{_configuration["AppUrl"]}/confirm-email?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

            await SendEmail(
                user.Email,
                "Confirm your email",
                $"Click the link to confirm your email: {confirmationLink}"
            );


            return new
            {
                success = true,
                message = "User created. Please confirm your email."
            };
        }

        // ================= CONFIRM EMAIL =================
        public async Task<string> ActivateAsync(ActivateDto activateDto)
        {
            var user = await _userManager.FindByEmailAsync(activateDto.Email);

            if (user == null)
                return "Invalid request";

            var result = await _userManager.ConfirmEmailAsync(user, activateDto.Token);

            return result.Succeeded
                ? "Email confirmed successfully"
                : string.Join(", ", result.Errors.Select(e => e.Description));
        }

        // ================= LOGIN =================
        public async Task<object> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return new AuthResponse
                {
                    Message = "check invalid Login"
                };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return (new
                {
                    success = false,
                    message = "Please confirm your email first"
                });
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginDto.Password,
                false
            );

            if (!result.Succeeded)
                return (new
                {
                    success = false,
                    message = "Invalid login attempt."
                });

            // هنا لاحقًا هتحط JWT
            var token = genToken.GetandCreateToken(user);

            return (new
            {
                success = true,
                message = "Logged in successfully",
                username = user.UserName,

                token = token


            });
        }

        // ================= SEND EMAIL =================
        public async Task SendEmail(string email, string subject, string content)
        {
            var emailDto = new EmailDto(
                email,
                _configuration["EmailSettings:From"],
                subject,
                content
            );

            await _emailService.SendEmailAsync(emailDto);
        }

        // ================= FORGOT PASSWORD =================
        public async Task<object> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return new
                {
                    success = false,
                    message = "If this email is registered, a reset link will be sent."
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink =
                $"{_configuration["AppUrl"]}/reset-password?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

            await SendEmail(
                user.Email,
                "Password Reset Request",
                $"Click the link to reset your password: {confirmationLink}"
            );

            return (new
            {
                success = true,
                message = "check your email for reset link."
            });
        }

        // ================= RESET PASSWORD =================
        public async Task<string> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return "Invalid request.";

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword
            );

            return result.Succeeded
                ? "Password has been reset successfully."
                : string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<bool> UpdateAddress(string email, Address address)
        {
            var foundUser = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (foundUser == null)
            {
                return false;
            }

            var myaddress = _dbContext.Addresses.FirstOrDefault(a => a.AppUserId == foundUser.Id);
            if (myaddress == null)
            {
                return false;
            }
            else
            {

                myaddress.Street = address.Street;
                myaddress.City = address.City;
                myaddress.State = address.State;
                myaddress.ZipCode = address.ZipCode;

                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}