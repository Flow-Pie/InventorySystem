using Microsoft.AspNetCore.Identity;
namespace Application.Extension.Identity;
public class ApplicationUser : IdentityUser
{
    public string Name {get; set;}    
}