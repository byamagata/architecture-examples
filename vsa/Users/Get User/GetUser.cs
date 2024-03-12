using Microsoft.AspNetCore.Identity;

namespace Vsa.Users.GetUser;


public record GetUserResponse(string Id, string Name, string Email); // Phone Number omitted due to privacy concerns
public class GetUserEndpoint
{
    public static async Task<IResult> GetUserAsync(IGetUserManager getUserManager, string userId)
    {
        var user = await getUserManager.GetUserAsync(userId);
        return Results.Ok(new GetUserResponse(user.Id, user.Name, user.Email));
    }
}

public interface IGetUserManager
{
    Task<User> GetUserAsync(string userId);
}

public class GetUserManager([FromKeyedServices("GetUserStore")]IUserStore _userStore) : IGetUserManager
{
    public async Task<User> GetUserAsync(string userId)
    {
        return await _userStore.GetUserAsync(userId);
    }
}

public record User(string Id, string Name, string Email, string PhoneNumber);

public interface IUserStore
{
    Task<User> GetUserAsync(string userId);
}

public class UserStore : IUserStore
{
    public async Task<User> GetUserAsync(string userId) =>
        // Get user from database
        await Task.FromResult(
            MockUserStore.Users.Where(u => u.Id == userId)
                                ?.Select(u => new User(u.Id, u.Name, u.Email, u.PhoneNumber))
                                .FirstOrDefault())
                                ?? throw new Exception("User not found");
}

