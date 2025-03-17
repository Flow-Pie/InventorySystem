// using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.ActivityTracker;
using Application.DTO.Response.Identity;
using Application.Interfaces.Identity;

namespace Application.Services.Identity;

public class AccountService(IAccount account) : IAccountService
{
    public async Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model)
        => await account.CreateUserAsync(model);

    public async Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUsersWithClaimsAsync()
        => await account.GetUsersWithClaimsAsync();

    public async Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model)
        => await account.LoginAsync(model);

    public async Task SetUpAsync() => await account.SetUpAsync();
    
    public async Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model)
        => await account.UpdateUserAsync(model);

    private async Task<IEnumerator<ActivityTrackerResponseDTO>> GetActivitiesAsync()
        => (await account.GetActivitiesAsync()).GetEnumerator();
    public async Task SaveActivityAsync(ActivityTrackerRequestDTO model)
        => await account.SaveActivityAsync(model);

    public async Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDTO>>> GetActivitiesByDateAsync()
        {
            var enumerator = await GetActivitiesAsync();
            var activities = new List<ActivityTrackerResponseDTO>();
            while (enumerator.MoveNext())
            {
                activities.Add(enumerator.Current);
            }
            var data = activities.GroupBy(e => e.Date).AsEnumerable();
            return (IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDTO>>)data;
        }
   
}