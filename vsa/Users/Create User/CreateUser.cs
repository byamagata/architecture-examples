using Microsoft.AspNetCore.Mvc;

namespace Vsa.Users.CreateUser;

public record CreateUserRequest(string Name = "", string Email = "", string PhoneNumber = "");
public record CreateUserResponse(string Id);

public class CreateUserEndpoint
{
    public static async Task<IResult> CreateUserAsync([FromBody] CreateUserRequest request, [FromServices] ICreateUserManager createUserManager)
    {   
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Results.BadRequest("Name is required");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return Results.BadRequest("Email is required");
        }

        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return Results.BadRequest("PhoneNumber is required");
        }

        var id = await createUserManager.CreateUserAsync(new CreateUserDTO(request.Name, request.Email, request.PhoneNumber));
        return Results.Ok(new CreateUserResponse(id));
    }
}

public record CreateUserDTO(string Name, string Email, string PhoneNumber);

public interface ICreateUserManager
{
    Task<string> CreateUserAsync(CreateUserDTO createUserDTO);
}

public class CreateUserManager([FromKeyedServices("CreateUserStore")] IUserStore userStore) : ICreateUserManager
{
    public async Task<string> CreateUserAsync(CreateUserDTO userInfo)
    {
        var newUserId = Guid.NewGuid().ToString();
        var newUser = new User(newUserId, userInfo.Name, userInfo.Email, userInfo.PhoneNumber);
        await userStore.CreateUserAsync(newUser);
        return newUserId;
    }
}

public record User(string Id, string Name, string Email, string PhoneNumber);

public interface IUserStore
{
    Task<bool> CreateUserAsync(User user);
}

public class UserStore : IUserStore
{
    public async Task<bool> CreateUserAsync(User user)
    {
        // Create user in database
        var newUser = new MockUserStoreConnection.User(user.Id, user.Name, user.Email, user.PhoneNumber);
        MockUserStoreConnection.Users.Add(newUser);
        return await Task.FromResult(true);
    }
}