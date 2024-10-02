using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Service.Supplier.Interface;
using opcs.Resources;

namespace opcs.App.Controller.Supply;

[Route("supplier")]
[ApiController]
[Authorize]
public class SupplierController(ISupplierService supplierService)
    : ControllerBase
{
    [HttpGet("getAll")]
    [AllowAnonymous]
    public IActionResult GetAllSupplier()
    {
        return new Response
        {
            dto = supplierService.GetAllSupplier()
        };
    }

    [HttpGet]
    public IActionResult GetSupplierById([FromQuery(Name = "id")] int id)
    {
        return supplierService.GetSupplierById(id).Match(
            result => new Response { dto = result },
            () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_supplier_not_exist
            }
        );
    }
}