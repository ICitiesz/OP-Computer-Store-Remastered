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

    [HttpPost("account/update")]
    public IActionResult UpdateAccount()
    {
        return new Response();
    }
}