using Task_Tracker.Models.Entities;

namespace Task_Tracker.Models.DTO
{
    public class EditReadTaskDto
    {

        //The data transfer objects ensures that only required data is being accessed
        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Status { get; set; } = "Pending";


    }
}
