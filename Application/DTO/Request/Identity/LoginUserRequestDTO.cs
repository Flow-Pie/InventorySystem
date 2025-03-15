using System.ComponentModel.DataAnnotations;
namespace Application.DTO.Request.Identity{
public class LoginUserRequestDTO{
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                            ErrorMessage = "Password must be at least 8 characters long and include an uppercase letter, a lowercase letter, a number, and a special character.")]
        public string Password { get; set; }

    }
}