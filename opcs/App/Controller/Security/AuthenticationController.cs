using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Data.Dto.Request;
using opcs.App.Service.Security.Interface;
using opcs.Resources;

namespace opcs.App.Controller.Security;

[Authorize]
[Route("auth")]
[ApiController, Produces("application/json")]
public class AuthenticationController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] UserRegisterRequestDto requestDto)
    {
        return authService.UserRegister(requestDto).Match(
            Some: result => new Response
            {
                dto = result,
                message = CodeMessages.opcs_info_auth_registration_successful
            },
            None: () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_user_user_exist
            }
        );
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login([FromBody] UserLoginRequestDto requestDto)
    {
        return authService.UserLogin(requestDto).Match(
            Some: result =>
                new Response
                {
                    dto = result,
                    message = CodeMessages.opcs_info_auth_login_successful
                },
            None: () => new Response
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
    [HttpGet("refresh")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequestDto requestDto)
    {
        return authService.RefreshAuthentication(requestDto).Match(
            Some: result => new Response
                {
                    dto = result,
                    message = CodeMessages.opcs_info_auth_session_refresh
                },
            None: () => new Response
            {
                statusCode = StatusCodes.Status401Unauthorized,
                message = CodeMessages.opcs_error_auth_unauthorized
            }
        );
    }
}