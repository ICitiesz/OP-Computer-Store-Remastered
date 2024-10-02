using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Service.User.Interface;

namespace opcs.App.Controller.User;

[Authorize]
[Route("user")]
[ApiController]
[Produces("application/json")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("account/update")]
    public IActionResult UpdateAccount()
    {
        return new Response();
    }
}