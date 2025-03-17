using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Response.ActivityTracker ;
public class ActivityTrackerResponseDTO
{
  
    [Required]
    public string UserName { get; set; }

    [Required]
    public DateTime Date { get; set; }
    
}
