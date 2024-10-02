using LanguageExt;
using opcs.App.Data.Dto.Request;
using opcs.App.Data.Dto.Response;

namespace opcs.App.Service.Security.Interface;

public interface IAuthService
{
    Option<AuthenticationResponseDto> UserRegister(UserRegisterRequestDto requestDto);

    Option<AuthenticationResponseDto> UserLogin(UserLoginRequestDto requestDto);

    Option<AuthenticationResponseDto> RefreshAuthentication(RefreshTokenRequestDto requestDto);

    bool RevokeRefreshToken(string userId);

    bool RevokeRefreshToken(HttpContext httpContext);

    void SaveAuthCookies(IResponseCookies cookies, AuthenticationResponseDto authResponse);
}