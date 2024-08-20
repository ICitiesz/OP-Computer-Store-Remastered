using LanguageExt;
using opcs.App.Data.Dto.General;
using opcs.App.Data.Dto.Request;
using opcs.App.Data.Dto.Response;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.Security.Interface;
using opcs.App.Service.User.Interface;

namespace opcs.App.Service.User;

public class UserService(IUserRepository userRepository, ISecurityService securityService): IUserService
{
    public Option<AuthenticationResponseDto> RegisterUser(UserRegisterRequestDto requestDto)
    {
        if (userRepository.IsUserExistByUsernameEmail(requestDto.Username, requestDto.Email))
        {
            return new Option<AuthenticationResponseDto>();
        }

        Entity.User.User user = new()
        {
            UserId = securityService.GenerateUserId(),
            Username = requestDto.Username,
            Email = requestDto.Email
        };

        user.HashedPass = securityService.HashPassword(user, requestDto.ConfirmPassword);

        var result = userRepository.RegisterUserAsync(user).Result;

        return result == null ? new Option<AuthenticationResponseDto>() : AuthenticateUser(result);
    }

    public Option<AuthenticationResponseDto> LoginUser(UserLoginRequestDto requestDto)
    {
        var user = userRepository.GetUserByUsernameOrEmail(requestDto.UsernameOrEmail).Result;

        if (user is null) return new Option<AuthenticationResponseDto>();

        var isSamePassword = securityService.VerifyHashedPassword(user, requestDto.Password, user.HashedPass);

        return !isSamePassword ? new Option<AuthenticationResponseDto>() : AuthenticateUser(user);
    }

    private AuthenticationResponseDto AuthenticateUser(Entity.User.User user)
    {
        var accessToken = securityService.GenerateToken(user);

        return new AuthenticationResponseDto
        {
            AccessToken = accessToken,
            ExpiredAt = DateTime.UtcNow.AddMinutes(15),
            RefreshToken = string.Empty
        };
    }
}