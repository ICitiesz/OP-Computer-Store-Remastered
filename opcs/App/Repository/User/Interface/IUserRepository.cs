namespace opcs.App.Repository.User.Interface;

public interface IUserRepository
{
    Task<List<Entity.User.User>> GetAllUserAsync();

    Task<Entity.User.User?> GetUserByUuidCharAsync(string uuidChar);

    Task<Entity.User.User?> GetUserByUsernameOrEmail(string usernameOrEmail);

    Task<Entity.User.User?> RegisterUserAsync(Entity.User.User user);

    bool IsUserExistByUuidChar(string uuidChar);

    bool IsUserExistByUsernameEmail(string username, string email);
}