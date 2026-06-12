using AutoMapper;
using ECom.Core.DTO;
using ECom.Core.Entites;
using ECom.Core.Interface;
using ECom.Core.Services;
using ECom.infrastrucrure.Data;
using ECom.infrastrucrure.Repositries.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        public IEmailService _emailService { get; }
        public AppDbContext AppContext1 { get; }
        private readonly UserManager<AppUser> _userManager;
        public IAuth Auth { get; }      
        public AuthController(IAuth auth, IConfiguration configuration,
            IEmailService emailService 
            , AppDbContext appContext , UserManager<AppUser> userManager)
        {
            Auth = auth;
            _configuration = configuration;
            _emailService = emailService;
            AppContext1 = appContext;
            _userManager = userManager; 
        }


        [Authorize]

        [HttpPut("address")]
        public async Task<bool> UpdateAddress(ShippingAddressDto shippingAddressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var foundUser = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (foundUser == null)
                return false;

            var myaddress = AppContext1.Addresses.FirstOrDefault(a => a.AppUserId == foundUser.Id);

            if (myaddress == null)
            {
                // ✅ مفيش address → ضيف جديد
                var address = new Address
                {
                    AppUserId = foundUser.Id,
                    FirstName = shippingAddressDto.FirstName,
                    LastName = shippingAddressDto.LastName,
                    Street = shippingAddressDto.Street,
                    City = shippingAddressDto.City,
                    State = shippingAddressDto.State,
                    ZipCode = shippingAddressDto.ZipCode,
                    Country = shippingAddressDto.Country,
                    PostalCode = shippingAddressDto.PostalCode

                };
                await AppContext1   .Addresses.AddAsync(address);
            }
            else
            {
                // ✅ موجود → عدّل
                myaddress.FirstName = shippingAddressDto.FirstName;
                myaddress.LastName = shippingAddressDto.LastName;
                myaddress.Street = shippingAddressDto.Street;
                myaddress.City = shippingAddressDto.City;
                myaddress.State = shippingAddressDto.State;
                myaddress.ZipCode = shippingAddressDto.ZipCode;
                myaddress.Country = shippingAddressDto.Country;
                myaddress.PostalCode = shippingAddressDto.PostalCode;
                AppContext1.Addresses.Update(myaddress);
            }

            await AppContext1.SaveChangesAsync();
            return true;
        }
        //public async Task <bool> UpdateAddress(ShippingAddressDto shippingAddressDto)
        //{
        //    var email = User.FindFirstValue(ClaimTypes.Email);
        //    var address = new Address
        //    {
        //        FirstName = shippingAddressDto.FirstName,
        //        LastName = shippingAddressDto.LastName,
        //        Street = shippingAddressDto.Street,
        //        City = shippingAddressDto.City,
        //        State = shippingAddressDto.State,
        //        ZipCode = shippingAddressDto.ZipCode
        //    };
        //    var result = await Auth.UpdateAddress( email, address );
        //    if (!result)
        //        return false;
        //    return true;
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegDto regDto)
        {
            var result = await Auth.RegisterAsync(regDto);
            return Ok(result);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            if (emailDto == null)
                return BadRequest("Email data is required");

            // override From من appsettings
            emailDto.From = _configuration["EmailSettings:From"];

            await _emailService.SendEmailAsync(emailDto);

            return Ok(new { message = "Email sent successfully" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await Auth.LoginAsync(loginDto);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await Auth.ForgotPasswordAsync(dto);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await Auth.ResetPasswordAsync(dto);
            if (result != "Password has been reset successfully.")
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateDto dto)
        {
            var result = await Auth.ActivateAsync(dto);

            if (result != "Email confirmed successfully")  // ✅ نفس الـ string بالظبط
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }
    }
}
