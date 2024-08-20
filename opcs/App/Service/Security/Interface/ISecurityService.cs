namespace opcs.App.Service.Security.Interface;

public interface ISecurityService
{
    string HashPassword(Entity.User.User user, string password);

    bool VerifyHashedPassword(Entity.User.User user, string providedPassword, string hashedPassword);

    string GenerateUserId();

    string GenerateToken(Entity.User.User user);
}