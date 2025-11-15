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
            //Verify userId from token
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized("User id claim not found.");
            }

            //store userId after parsing
            var userId = int.Parse(userIdClaim.Value);


            //Create new instance of the task initally that the user inputted and automatically input the userId
            var task = new TaskItem
            {
                Title = dto.Title,
                Description=dto.Description,
                UserId = userId,
            };

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            return Ok($"{task.Title} has been added!" );
        }

        [HttpGet("view")]
        public IActionResult ViewTasks()
        {
            //Verify userId from token
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized("User id claim not found.");
            }

            //store userId after parsing
            var userId = int.Parse(userIdClaim.Value);

            var userTasks = dbContext.Tasks.Where(t => t.UserId == userId)
                .Select(t => new EditReadTaskDto
                {
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status
                }).ToList();

            return Ok(userTasks);
        }



    }
}
