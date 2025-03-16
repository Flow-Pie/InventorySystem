using System.Diagnostics;
using System.Security.Claims;
// using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.Identity;
using Application.Extension.Identity;
using Application.Interfaces.Identity;
// using Domain.Entities.ActivityTracker;
using Infrastructure.DataAccess;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class Account : IAccount
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _context;

    public Account(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model)
    {
        var user = await FindUserByEmail(model.Email);
        if (user != null)
            return new ServiceResponse(false, "User already exists.");

        var newUser = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name
        };

        // Pass the plain text password to CreateAsync
        var result = CheckResult(await _userManager.CreateAsync(newUser, model.Password));
        if (!result.Flag)
            return result;
        else
            return await CreateUserClaims(model);
    }


    private async Task<ServiceResponse> CreateUserClaims(CreateUserRequestDTO model)
    {
        if (string.IsNullOrEmpty(model.Policy)) return new ServiceResponse(false, "Policy is required. None Specified");
        Claim[] userClaims = [];
        if (model.Policy.Equals(Policy.AdminPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userClaims =
            [
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, "Manager"),
                new Claim("Create", "true"),
                new Claim("Update", "true"),
                new Claim("Delete", "true"),
                new Claim("Read", "true"),
                new Claim("ManageUser", "true")
            ];
        }
        else if (model.Policy.Equals(Policy.ManagerPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userClaims =
            [
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, "Manager"),
                new Claim("Create", "true"),
                new Claim("Update", "true"),
                new Claim("Delete", "false"),
                new Claim("Read", "true"),
                new Claim("ManageUser", "false")
            ];
        }
        else if (model.Policy.Equals(Policy.UserPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userClaims =
            [
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("Name", model.Name),
                new Claim("Create", "false"),
                new Claim("Update", "false"),
                new Claim("Delete", "false"),
                new Claim("Read", "false"),
                new Claim("ManageUser", "false")
            ];
        }
        var result = CheckResult(await _userManager.AddClaimsAsync(await FindUserByEmail(model.Email), userClaims));
        if (result.Flag)
            return new ServiceResponse(true, "User Created Successfully");
        else
            return result;
    }

    public async Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model)
    {
        var user = await FindUserByEmail(model.Email);
        if (user is null)
            return new ServiceResponse(false, "User not found");

        var verifyPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!verifyPassword.Succeeded)
            return new ServiceResponse(false, "Invalid credentials");

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (!result.Succeeded)
            return new ServiceResponse(false, "Unknown server error");
        return new ServiceResponse(true, null);
    }

    private async Task<ApplicationUser> FindUserByEmail(string email)
        => await _userManager.FindByEmailAsync(email);

    private async Task<ApplicationUser> FindUserById(string id)
        => await _userManager.FindByIdAsync(id);

    private static ServiceResponse CheckResult(IdentityResult result)
    {
        if (result.Succeeded)
            return new ServiceResponse(true, null);
        else
        {
            var errors = result.Errors.Select(_ => _.Description);
            return new ServiceResponse(false, string.Join(Environment.NewLine, errors));
        }
    }

    public async Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUsersWithClaimsAsync()
    {
        var UserList = new List<GetUserWithClaimResponseDTO>();
        var allUsers = await _userManager.Users.ToListAsync();
        if (allUsers.Count == 0) return UserList;

        foreach (var user in allUsers)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);
            var getCurrentUserClaims = await _userManager.GetClaimsAsync(currentUser);

            if (getCurrentUserClaims.Any())
                UserList.Add(new GetUserWithClaimResponseDTO()
                {
                    UserId = user.Id,
                    Email = getCurrentUserClaims.FirstOrDefault(_ => _.Type == ClaimTypes.Email)?.Value,
                    RoleName = getCurrentUserClaims.FirstOrDefault(_ => _.Type == ClaimTypes.Role)?.Value,
                    Name = getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Name")?.Value,
                    ManageUser = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "ManageUser")?.Value),
                    Create = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Create")?.Value),
                    Update = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Update")?.Value),
                    Delete = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Delete")?.Value),
                    Read = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Read")?.Value)
                });
        }
        return UserList;
    }

    public async Task SetUpAsync()
    {
        var adminEmail = "admin@admin.com";
        var adminUser = await FindUserByEmail(adminEmail);

        if (adminUser == null)
        {
            var createUserModel = new CreateUserRequestDTO
            {
                Name = "Administrator",
                Email = adminEmail,
                Password = "Admin@123", 
                Policy = Policy.AdminPolicy
            };

            var result = await CreateUserAsync(createUserModel);
            if (!result.Flag)
            {
                // Log the error or handle it as needed
                throw new Exception($"Failed to create admin user: {result.Message}");
            }
        }
        else
        {
            // Log that the admin user already exists
            Console.WriteLine("Admin user already exists.");
        }
    }

    public async Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null)
            return new ServiceResponse(false, "User not found");

        var oldUserClaims = await _userManager.GetClaimsAsync(user);
        Claim[] newUserClaims =
        [
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(ClaimTypes.Role, model.RoleName),
            new Claim("Name", model.Name),
            new Claim("Create", model.Create.ToString()),
            new Claim("Update", model.Update.ToString()),
            new Claim("Delete", model.Delete.ToString()),
            new Claim("Read", model.Read.ToString()),
            new Claim("ManageUser", model.ManageUser.ToString())
        ];

        var result = await _userManager.RemoveClaimsAsync(user, oldUserClaims);
        var response = CheckResult(result);
        if (!response.Flag)
            return new ServiceResponse(false, response.Message);

        var addNewClaims = await _userManager.AddClaimsAsync(user, newUserClaims);
        var Outcome = CheckResult(addNewClaims);
        if (Outcome.Flag)
            return new ServiceResponse(true, "User Updated Successfully");
        return Outcome;
    }

    // public async Task<IEnumerable<ActivityTrackerResponseDTO>> GetActivityAsync()
    // {
    //     var data = (await _context.ActivityTracker.ToListAsync()).Adapt<List<ActivityTrackerResponseDTO>>();

    //     foreach (var activity in data)
    //     {
    //         activity.Username = (await FindUserById(activity.UserId)).Name;
    //     }

    //     return data;
    // }

    // public Task SaveActivityAsync(ActivityTrackerRequestDTO model)
    // {
    //     throw new NotImplementedException();
    // }
}