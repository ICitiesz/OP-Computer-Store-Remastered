

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace opcs.App.Controller.Security;

[Authorize]
[Route("permission")]
[ApiController, Produces("application/json")]
public class AccessPermissionController : ControllerBase
{


}