using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        public async Task<IActionResult> CreateTask(TaskDto dto)
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

            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();

            return Ok($"{task.Title} has been added!" );
        }

        [HttpGet("view")]
        public async Task<IActionResult> ViewTasks()
        {
            //Verify userId from token
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized("User id claim not found.");
            }

            
            //store userId after parsing
            var userId = int.Parse(userIdClaim.Value);

            // Left join users -> tasks (group join) so we always return the user and an empty Tasks list if none exist
            var userWithTasks = await dbContext.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .GroupJoin(
                    dbContext.Tasks.AsNoTracking(),
                    u => u.Id,
                    t => t.UserId,
                    (u, tasks) => new
                    {
                        Username = u.Username,
                        Tasks = tasks.Select(t => new EditReadTaskDto
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            DueDate = t.DueDate,
                            Status = t.Status
                        })
                    }
                )
                .ToListAsync();

            // Return single user object (or NotFound if user not found)
            var result = userWithTasks.FirstOrDefault();
            if (result == null) return NotFound("User not found.");

            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateTasks(UpdateTaskDto dto)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized("User id claim not found.");
            }

            //store userId after parsing
            var userId = int.Parse(userIdClaim.Value);

            // Load existing task and ensure it belongs to the current user
            var existing = dbContext.Tasks.FirstOrDefault(t => t.Id == dto.Id && t.UserId == userId);
            if (existing == null)
            {
                return NotFound("Task not found or you do not have permission to modify it.");
            }

            // Update only allowed fields
            existing.Title = dto.Title;
            existing.Description = dto.Description;

            await dbContext.SaveChangesAsync();

            return Ok($"{existing.Title} has been updated!");

        }



    }
}
