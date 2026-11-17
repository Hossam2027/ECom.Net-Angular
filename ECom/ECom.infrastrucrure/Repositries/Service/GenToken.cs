using ECom.Core.Entites;
using ECom.Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries.Service
{
    public class GenToken : IGenToken
    {
        public IConfiguration _Configuration { get; }

        public GenToken(IConfiguration configuration)
        {
            _Configuration = configuration;
        }


        public string GetandCreateToken(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>()
            {
                //new Claim("email", appUser.Email),

               new Claim(ClaimTypes.Name, appUser.Name),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id)
            };
            var key = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(_Configuration["JWT:Key"])
);
            var creds = new SigningCredentials(
    key,
    SecurityAlgorithms.HmacSha256
);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _Configuration["JWT:Issuer"],
                Audience = _Configuration["JWT:Audience"], 

                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;

        }
    }
}
