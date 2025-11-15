using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using Task_Tracker.Data;
using Task_Tracker.Models.DTO;
using Task_Tracker.Models.Entities;
using Task_Tracker.Services;

namespace Task_Tracker.Controllers
{
    
    
    // localhost:xxxx/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly JwtTokenClass jwtTokenClass;
       
        public UsersController(ApplicationDbContext dbContext, JwtTokenClass jwtTokenClass)
        {
            this.dbContext = dbContext;
            this.jwtTokenClass = jwtTokenClass;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto dto)
        
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                HashedPassword = hashed
            };

            dbContext.Users.Add(user);

            await dbContext.SaveChangesAsync();


            return Ok("User created");

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user= dbContext.Users.FirstOrDefault(u => u.Username==dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password,user.HashedPassword))
            {
                return Unauthorized("Invalid Credentials");
            }

            var token = jwtTokenClass.GenerateToken(user);

            return Ok(new { token });


        }




        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            var allUsers=dbContext.Users.ToList(); 
            return Ok(allUsers);
        }
    }

 
}
