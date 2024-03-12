using Microsoft.AspNetCore.Mvc;

namespace Vsa.Users.UpdateUser;

public record UpdateUserRequest(string Name, string Email, string PhoneNumber);

public class UpdateUserEndpoint
{
    public static async Task<IResult> UpdateUserAsync([FromRoute] string id, [FromBody] UpdateUserRequest request, [FromServices] IUpdateUserManager updateUserManager)
    {
        var user = new UpdateUserDTO(id, request.Name, request.Email, request.PhoneNumber);
        await updateUserManager.UpdateUserAsync(user);
        return Results.Ok();
    }
}

public record UpdateUserDTO(string Id, string Name, string Email, string PhoneNumber);

public interface IUpdateUserManager
{
    Task UpdateUserAsync(UpdateUserDTO updatedUser);
}

public class UpdateUserManager([FromKeyedServices("UpdateUserStore")] IUserStore _userStore) : IUpdateUserManager
{
    public async Task UpdateUserAsync(UpdateUserDTO updatedUser)
    {
        await _userStore.UpdateUserAsync(new User(updatedUser.Id, updatedUser.Name, updatedUser.Email, updatedUser.PhoneNumber));
    }
}

public record User(string Id, string Name, string Email, string PhoneNumber);

public interface IUserStore
{
    Task<bool> UpdateUserAsync(User updatedUser);
}

public class UserStore : IUserStore
{
    public async Task<bool> UpdateUserAsync(User updatedUser)
    {
        // Update user in database
        MockUserStore.Users.RemoveAll(u => u.Id == updatedUser.Id);
        MockUserStore.Users.Add(new MockUserStore.User(updatedUser.Id, updatedUser.Name, updatedUser.Email, updatedUser.PhoneNumber));
        return await Task.FromResult(true);
    }
}


