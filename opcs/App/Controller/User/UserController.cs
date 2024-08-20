using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Data.Dto.Request;
using opcs.App.Service.User.Interface;
using AppContext = opcs.App.Common.AppContext;

namespace opcs.App.Controller.User;

[Authorize]
[Route("user")]
[ApiController, Produces("application/json")]
public class UserController(IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("auth/register")]
    public IActionResult RegisterUser([FromBody] UserRegisterRequestDto requestDto)
    {
        return userService.RegisterUser(requestDto).Match(
            Some: result => new Response(dto: result, message: AppContext.GetCodeMessage("opcs.info.user.registration_successful")),
            None: () => new Response(
                null,
                statusCode: StatusCodes.Status400BadRequest,
                message: AppContext.GetCodeMessage("opcs.error.user.user_exist")
                )
        );
    }


    [AllowAnonymous]
    [HttpGet("auth/login")]
    public IActionResult Login([FromBody] UserLoginRequestDto requestDto)
    {
        return userService.LoginUser(requestDto).Match(
            Some: result =>
                new Response(dto: result, message: AppContext.GetCodeMessage("opcs.info.user.login_successful")),
            None: () => new Response(
                null,
                statusCode: StatusCodes.Status400BadRequest,
                message: AppContext.GetCodeMessage("opcs.error.user.login_failed")
            )
        );
    }

    [HttpGet("auth/refresh")]
    public IActionResult RefreshToken()
    {
        return new Response();
    }

    [HttpPost("account/update")]
    public IActionResult UpdateAccount()
    {
        return new Response();
    }
}