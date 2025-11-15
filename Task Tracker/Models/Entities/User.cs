using Microsoft.EntityFrameworkCore;

namespace Task_Tracker.Models.Entities
{
    public class User
    {

        public int Id { get; set; }
  
        public required string Username { get; set; }

        public required string HashedPassword { get; set; }


    }
}
