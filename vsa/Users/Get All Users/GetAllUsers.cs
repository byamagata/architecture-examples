namespace Vsa.Users.GetAllUsers;


public record GetAllUserResponse(string Id, string Name, string Email); // Phone Number omitted due to privacy concerns
public class GetAllUsersEndpoint
{
    public static async Task<IResult> GetAllUsersAsync(IGetAllUsersManager getAllUsersManager)
    {
        var users = await getAllUsersManager.GetAllUsersAsync();
        return Results.Ok(users.Select(u => new GetAllUserResponse(u.Id, u.Name, u.Email)));
    }
}

public interface IGetAllUsersManager
{
    Task<List<User>> GetAllUsersAsync();
}

public class GetAllUsersManager([FromKeyedServices("GetAllUsersStore")]IUserStore _userStore) : IGetAllUsersManager
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userStore.GetAllUsersAsync();
    }
}

public record User(string Id, string Name, string Email, string PhoneNumber);

public interface IUserStore
{
    Task<List<User>> GetAllUsersAsync();
}

public class UserStore : IUserStore
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        // Get all users from database
        return await Task.FromResult(new List<User>());
    }
}
