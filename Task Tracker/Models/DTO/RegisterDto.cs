using System.ComponentModel.DataAnnotations;

namespace Task_Tracker.Models.DTO
{
    public class RegisterDto
    {
        //The data transfer objects ensures that only required data is being accessed

        public required string Username {  get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
ErrorMessage = "Password must contain uppercase, lowercase, and a number.")]
        public required string Password { get; set; }
    }
}
