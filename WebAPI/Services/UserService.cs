using System.Globalization;
using Dapper;
using Database.SQLHelper;
using Microsoft.AspNetCore.Mvc;
using Shared.Helpers;
using Shared.Models;
using WebAPI.Services;

public class UserService : IUserService
{
    private readonly SqlConnectionClass _sqlConnectionClass;

    public UserService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    public async Task<string> CreateUser(UserModelDto userModelDto)
    {
        var userExist = await GetUser(userModelDto.Email);

        if (userExist != null)
        {
            return $"User: {userExist.Email} already exists";
        }

        var hashPassword = Hashing.HashString(userModelDto.Password);
        var registerUser = await RegisterUser(userModelDto, hashPassword);

        return registerUser;
    }

    public async Task<IActionResult> LoginUser(UserModelDto userModelDto)
    {
        bool validateLogin = await CheckPassword(userModelDto.Email, Hashing.HashString(userModelDto.Password));

        if (!validateLogin)
        {
            return new BadRequestObjectResult("Password incorrect!");
        }

        var token = Hashing.HashString(userModelDto.Email, DateTime.Now.ToString(CultureInfo.InvariantCulture));
        userModelDto.Token = token;

        await AddTokenToUser(userModelDto.Email, token);

        return new OkObjectResult("Login successful!");
    }

    public async Task<IActionResult> LogoutUser(string email)
    {
        var getUser = await GetUser(email);

        if (getUser == null || string.IsNullOrEmpty(getUser.Token))
        {
            return new NotFoundObjectResult("User not logged in");
        }

        await RemoveTokenFromUser(email);

        return new OkObjectResult("Logged out");
    }

    private async Task<string> RegisterUser(UserModelDto userModelDto, string hashPassword)
    {
        try
        {
            string query = $"INSERT INTO Operator (Email, Password, FirstName, LastName, Role, Token)" +
                           $"VALUES ('{userModelDto.Email}','{hashPassword}','{userModelDto.FirstName}','{userModelDto.LastName}','user', null)";

            await using var connection = _sqlConnectionClass.GetConnection();
            connection.Query(query);

            return "User created!";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<UserModelDto?> GetUser(string email)
    {
        var query = $"SELECT Email, FirstName, LastName, Role, Token FROM Operator WHERE Email = '{email}'";

        await using var connection = _sqlConnectionClass.GetConnection();
        var userExist = connection.QuerySingleOrDefault<UserModelDto>(query);

        return userExist;
    }

    private async Task<bool> CheckPassword(string email, string password)
    {
        await using var connection = _sqlConnectionClass.GetConnection();
        var result = await connection.QueryFirstOrDefaultAsync<UserModelDto>(
            $"SELECT 1 FROM Operator WHERE Email = '{email}' AND Password = '{password}' ");

        return result != null;
    }

    private async Task RemoveTokenFromUser(string email)
    {
        try
        {
            await using var connection = _sqlConnectionClass.GetConnection();
            connection.Query($"UPDATE Operator SET Token = null WHERE Email = '{email}'");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task AddTokenToUser(string email, string token)
    {
        try
        {
            await using var connection = _sqlConnectionClass.GetConnection();
            connection.Query($"UPDATE Operator SET Token = '{token}' WHERE Email = '{email}'");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}