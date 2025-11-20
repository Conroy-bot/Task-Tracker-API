using Task_Tracker.Models.Entities;

namespace Task_Tracker.Models.DTO
{
    public class UpdateTaskDto
    {

        //The data transfer objects ensures that only required data is being accessed
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

      




    }
}
