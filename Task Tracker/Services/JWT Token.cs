using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Tracker.Models.Entities;

namespace Task_Tracker.Services
{
    public class JwtTokenClass
    {
            private readonly string Secret;
            private readonly double Expiration;

        public JwtTokenClass(string secret,double expiration)
        {
            Secret= secret;
            Expiration= expiration;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(Expiration),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }






    }

}
