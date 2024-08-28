using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Data.Dto.Request;
using opcs.App.Database;
using opcs.App.Repository.User.Interface;

namespace opcs.App.Repository.User;

public class UserRepository(AppDbContext dbContext): IUserRepository
{
    public Task<List<Entity.User.User>> GetAllUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Entity.User.User?> GetUserByUserId(string userId)
    {
        var user = await dbContext.User.FromSqlInterpolated(
            $"""
             SELECT *
             FROM t_user tUser
             WHERE tUser.user_id = {userId} LIMIT 1
             """).ToArrayAsync();

        return !user.IsNullOrEmpty() ? user.First() : null;
    }

    public async Task<Entity.User.User?> GetUserByUsernameOrEmail(string usernameOrEmail)
    {
        var user = await dbContext.User.FromSqlInterpolated(
            $"""
             SELECT *
             FROM t_user tUser
             WHERE tUser.username = {usernameOrEmail} OR tUser.email = {usernameOrEmail} LIMIT 1
             """).ToArrayAsync();

        return !user.IsNullOrEmpty() ? user.First() : null;
    }

    public async Task<Entity.User.User?> RegisterUserAsync(Entity.User.User user)
    {
        var result = await dbContext.User.AddAsync(user);

        return await dbContext.SaveChangesAsync() > 0 ? result.Entity : null;
    }

    public bool HasUserByUsernameEmail(string username, string email)
    {
        return dbContext.User.Exists(user => user.Username == username && user.Email == email);
    }

    public bool HasUserByUserId(string userId)
    {
        return dbContext.User.Exists(user => user.UserId == userId);
    }
}