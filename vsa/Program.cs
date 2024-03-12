using Vsa.Users;
using Vsa.Users.CreateUser;
using Vsa.Users.DeleteUser;
using Vsa.Users.GetAllUsers;
using Vsa.Users.GetUser;
using Vsa.Users.UpdateUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICreateUserManager, CreateUserManager>();
builder.Services.AddTransient<IDeleteUserManager, DeleteUserManager>();
builder.Services.AddTransient<IGetAllUsersManager, GetAllUsersManager>();
builder.Services.AddTransient<IGetUserManager, GetUserManager>();
builder.Services.AddTransient<IUpdateUserManager, UpdateUserManager>();
builder.Services.AddKeyedTransient<Vsa.Users.CreateUser.IUserStore, Vsa.Users.CreateUser.UserStore>("CreateUserStore");
builder.Services.AddKeyedTransient<Vsa.Users.DeleteUser.IUserStore, Vsa.Users.DeleteUser.UserStore>("DeleteUserStore");
builder.Services.AddKeyedTransient<Vsa.Users.GetAllUsers.IUserStore, Vsa.Users.GetAllUsers.UserStore>("GetAllUsersStore");
builder.Services.AddKeyedTransient<Vsa.Users.GetUser.IUserStore, Vsa.Users.GetUser.UserStore>("GetUserStore");
builder.Services.AddKeyedTransient<Vsa.Users.UpdateUser.IUserStore, Vsa.Users.UpdateUser.UserStore>("UpdateUserStore");

var app = builder.Build();

app.MapGroup("/users").MapUsersApi();

app.Run();


/// <summary>
/// Only for mocking purposes so that you can run this OTB.
/// </summary>
public class MockUserStoreConnection
{
    public record User(string Id, string Name, string Email, string PhoneNumber);
    public static List<User> Users { get; set; } = new List<User>();
}