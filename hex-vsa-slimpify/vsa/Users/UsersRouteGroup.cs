using Vsa.Users.CreateUser;
using Vsa.Users.DeleteUser;
using Vsa.Users.GetAllUsers;
using Vsa.Users.GetUser;
using Vsa.Users.UpdateUser;

namespace Vsa.Users;

public static class UsersRouteGroup
{
    public static RouteGroupBuilder MapUsersApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateUserEndpoint.CreateUserAsync);
        group.MapGet("/", GetAllUsersEndpoint.GetAllUsersAsync);
        group.MapDelete("/{userId}", DeleteUserEndpoint.DeleteUserAsync);
        group.MapGet("/{userId}", GetUserEndpoint.GetUserAsync);
        group.MapPut("/{userId}", UpdateUserEndpoint.UpdateUserAsync);
        return group;
    }
}