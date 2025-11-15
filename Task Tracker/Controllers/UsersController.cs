using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Task_Tracker.Data;
using Task_Tracker.Models.DTO;
using Task_Tracker.Models.Entities;

namespace Task_Tracker.Controllers
{
    // localhost:xxxx/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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




        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            var allUsers=dbContext.Users.ToList(); 
            return Ok(allUsers);
        }
    }
}
