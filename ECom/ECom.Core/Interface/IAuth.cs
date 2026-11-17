using ECom.Core.DTO;
using ECom.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public  interface IAuth
    {
        Task <object> RegisterAsync (RegDto regDto);
        Task<object> LoginAsync (LoginDto loginDto);
        Task<object> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<string> ResetPasswordAsync(ResetPasswordDto dto);
        Task<string> ActivateAsync (ActivateDto dto);
        Task<bool> UpdateAddress(string email , Address address);
    }
}
