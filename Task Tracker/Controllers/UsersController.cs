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
            //hashes the password that the user inputted (no format yet)
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            //created a new instance of the user based off of inputted details
            var user = new User
            {
                Username = dto.Username,
                HashedPassword = hashed
            };

            //stores to the database
            dbContext.Users.Add(user);

            //saves the changes to the database
            await dbContext.SaveChangesAsync();


            return Ok("User created");

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            //Brings up the first user that is found for username
            var user= dbContext.Users.FirstOrDefault(u => u.Username==dto.Username);

            //Verifies if the user exists and then checks if the password they inputted matches the hashed password in the database
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password,user.HashedPassword))
            {
                return Unauthorized("Invalid Credentials");
            }

            //Generate the token based off of the user details
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
