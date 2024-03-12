using Microsoft.AspNetCore.Mvc;

namespace Vsa.Users.DeleteUser;

public class DeleteUserEndpoint
{
    public static async Task<IResult> DeleteUserAsync([FromRoute] string userId, IDeleteUserManager deleteUserManager)
    {
        await deleteUserManager.DeleteUserAsync(userId);
        return Results.NoContent();
    }
}

public interface IDeleteUserManager
{
    Task DeleteUserAsync(string userId);
}

public class DeleteUserManager([FromKeyedServices("DeleteUserStore")] IUserStore _userStore) : IDeleteUserManager
{
    public async Task DeleteUserAsync(string userId)
    {
        await _userStore.DeleteUserAsync(userId);
    }
}

public interface IUserStore
{
    Task<bool> DeleteUserAsync(string userId);
}

public class UserStore : IUserStore
{
    public async Task<bool> DeleteUserAsync(string userId)
    {
        // Delete user from database
        MockUserStore.Users.RemoveAll(u => u.Id == userId);
        return await Task.FromResult(true);
    }
}