using Application.DTO.Response.Identity;

namespace Application.DTO.Response;

public class ActivityTrackerResponseDTO : BaseUserClaimsDTO
{
    public int Id { get; set; } 
    public string Activity { get; set; } 
    public string Username { get; set; } 
}