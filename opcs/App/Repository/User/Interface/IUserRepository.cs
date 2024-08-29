namespace opcs.App.Repository.User.Interface;

public interface IUserRepository
{
    Task<List<Entity.User.User>> GetAllUserAsync();

    Task<Entity.User.User?> GetUserByUserId(string userId);

    Task<Entity.User.User?> GetUserByUsernameOrEmail(string usernameOrEmail);

    Task<Entity.User.User?> RegisterUserAsync(Entity.User.User user);

    bool HasUserByUsernameEmail(string username, string email);

    bool HasUserByUserId(string userId);
}