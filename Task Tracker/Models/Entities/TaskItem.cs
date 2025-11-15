using Microsoft.EntityFrameworkCore;

namespace Task_Tracker.Models.Entities
{
    public class TaskItem
    {

        public int Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Status { get; set; } = "Pending";

        //FK

        public int UserId {  get; set; }
        public User? User { get; set; }


    }
}
