using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Core;
using opcs.App.Data;
using opcs.App.Data.Dto.Request;
using opcs.App.Service.Security.Interface;
using opcs.Resources;

namespace opcs.App.Controller.Security;

[Authorize]
[Route("auth")]
[ApiController]
[Produces("application/json")]
public class AuthenticationController(
    IAuthService authService,
    AppConfiguration appConfig
) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] UserRegisterRequestDto requestDto)
    {
        return authService.UserRegister(requestDto).Match(
            result => new Response
            {
                dto = result,
                message = CodeMessages.opcs_info_auth_registration_successful
            },
            () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_user_user_exist
            }
        );
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequestDto requestDto)
    {
        return authService.UserLogin(requestDto).Match(
            result =>
            {
                authService.SaveAuthCookies(HttpContext.Response.Cookies, result);

                return new Response
                {
                    message = CodeMessages.opcs_info_auth_login_successful
                };
            },
            () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_auth_login_failed
            }
        );
    }

    [HttpPost("logout")]
    public IActionResult RevokeRefreshToken()
    {
        authService.RevokeRefreshToken(HttpContext);
        return new Response();
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public IActionResult RefreshToken()
    {
        var httpOnlyCookies = HttpContext.Request.Cookies;

        if (httpOnlyCookies.IsNullOrEmpty()
            || !httpOnlyCookies.ContainsKey("accessToken")
            || !httpOnlyCookies.ContainsKey("refreshToken"))
            return new Response
            {
                statusCode = StatusCodes.Status401Unauthorized,
                message = CodeMessages.opcs_error_auth_unauthorized
            };

        var req = new RefreshTokenRequestDto(
            httpOnlyCookies["accessToken"]!,
            httpOnlyCookies["refreshToken"]!
        );

        return authService.RefreshAuthentication(req).Match(
            result =>
            {
                authService.SaveAuthCookies(HttpContext.Response.Cookies, result);

                return new Response
                {
                    message = CodeMessages.opcs_info_auth_session_refresh
                };
            },
            () => new Response
            {
                statusCode = StatusCodes.Status401Unauthorized,
                message = CodeMessages.opcs_error_auth_unauthorized
            }
        );
    }
}