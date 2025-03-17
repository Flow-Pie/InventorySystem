// using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.ActivityTracker;
using Application.DTO.Response.Identity;

namespace Application.Services;

public interface IAccountService{
    Task<ServiceResponse> LoginAsync(LoginUserRequestDTO request);
    Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO request);
    Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUsersWithClaimsAsync();
    Task SetUpAsync();
    Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model);
   Task SaveActivityAsync(ActivityTrackerRequestDTO model);
   Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDTO>>> GetActivitiesByDateAsync();

}