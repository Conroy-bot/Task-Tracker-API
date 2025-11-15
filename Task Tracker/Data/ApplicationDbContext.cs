using Microsoft.EntityFrameworkCore;
using Task_Tracker.Models.Entities;

namespace Task_Tracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        //represents how the models/classes are stored in the database
        

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
