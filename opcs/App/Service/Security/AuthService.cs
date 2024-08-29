
using LanguageExt;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Core;
using opcs.App.Core.Security;
using opcs.App.Data.Dto.General;
using opcs.App.Data.Dto.Request;
using opcs.App.Data.Dto.Response;
using opcs.App.Repository.Security.Interface;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.Security.Interface;

namespace opcs.App.Service.Security;

public class AuthService(
    IUserRepository userRepository,
    IAuthRefreshTokenRepository refreshTokenRepository,

    ISecurityService securityService,
    AppConfiguration appConfig) : IAuthService
{
    public Option<AuthenticationResponseDto> UserRegister(UserRegisterRequestDto requestDto)
    {
        if (userRepository.HasUserByUsernameEmail(requestDto.Username, requestDto.Email))
        {
            return new Option<AuthenticationResponseDto>();
        }

        var user = new Entity.User.User
        {
            UserId = securityService.GenerateUserId(),
            Username = requestDto.Username,
            Email = requestDto.Email
        };

        user.HashedPass = securityService.HashPassword(user, requestDto.ConfirmPassword);

        var result = userRepository.RegisterUserAsync(user).Result;

        return result == null ? new Option<AuthenticationResponseDto>() : AuthenticateUser(user);
    }

    public Option<AuthenticationResponseDto> UserLogin(UserLoginRequestDto requestDto)
    {
        var user = userRepository.GetUserByUsernameOrEmail(requestDto.UsernameOrEmail).Result;

        if (user is null) return new Option<AuthenticationResponseDto>();

        var isPasswordSame = securityService.VerifyHashedPassword(user, requestDto.Password, user.HashedPass);

        return !isPasswordSame ? new Option<AuthenticationResponseDto>() : AuthenticateUser(user);
    }

    public Option<AuthenticationResponseDto> RefreshAuthentication(RefreshTokenRequestDto requestDto)
    {
        var (isTokenValid, user) = securityService.ValidateAccessToken(requestDto.AccessToken);

        if (!isTokenValid) return new Option<AuthenticationResponseDto>();

        return !securityService.ValidateRefreshToken(user!.UserId, requestDto.RefreshToken) ? new Option<AuthenticationResponseDto>() : AuthenticateUser(user, true);
    }

    public bool RevokeRefreshToken(HttpContext httpContext)
    {
        var userId = httpContext.User.Claims
            .First(claim => claim.Type is JwtAuthClaims.UserId).Value;

        return RevokeRefreshToken(userId);
    }

    public bool RevokeRefreshToken(string userId)
    {
        return refreshTokenRepository.RevokeRefreshTokenAsync(userId).Result;
    }

    private AuthenticationResponseDto AuthenticateUser(Entity.User.User user, bool isRefreshAuth = false)
    {
        var accessToken = securityService.GenerateAccessToken(user);
        var refreshTokenDto = securityService.GenerateUserRefreshToken(user.UserId, isRefreshAuth);

        return new AuthenticationResponseDto(
            AccessToken: accessToken,
            Expiry: DateTime.UtcNow.AddMinutes(appConfig.GetAccessTokenLifeSpan()),
            RefreshToken: refreshTokenDto.Token
        );
    }
}