using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models.Dtos
{
    public class UserLoginRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
