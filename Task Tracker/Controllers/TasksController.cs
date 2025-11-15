using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Task_Tracker.Data;
using Task_Tracker.Models.DTO;
using Task_Tracker.Models.Entities;

namespace Task_Tracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public TasksController(ApplicationDbContext context)
        {
            dbContext = context;
        }


        
        [HttpPost("create")]
        public IActionResult CreateTask(TaskDto dto)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized("User id claim not found.");
            }

            var userId = int.Parse(userIdClaim.Value);

            var task = new TaskItem
            {
                Title = dto.Title
            };

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            return Ok($"{task.Title} has been added!" );






        }


    }
}
