using System.ComponentModel.DataAnnotations;
using Application.DTO.Request.Entity;
namespace Application.DTO.Request.Identity;

public class CreateUserRequestDTO : LoginUserRequestDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }    
    [Required]
    public string Policy { get; set; }
    public string PasswordHash { get; set; }
}