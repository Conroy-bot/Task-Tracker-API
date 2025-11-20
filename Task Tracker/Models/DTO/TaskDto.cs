using Task_Tracker.Models.Entities;

namespace Task_Tracker.Models.DTO
{
    public class TaskDto
    {

        
        //The data transfer objects ensures that only required data is being accessed
        public required string Title { get; set; }

        public string? Description { get; set; }

      




    }
}
