namespace Task_Tracker.Models.DTO
{
    public class RegisterDto
    {
        //The data transfer objects ensures that only required data is being accessed
        public required string Username {  get; set; }
        public required string Password { get; set; }
    }
}
