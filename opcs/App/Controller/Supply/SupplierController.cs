using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Service.Supplier.Interface;
using AppContext = opcs.App.Common.AppContext;

namespace opcs.App.Controller.Supply;

[Route("supplier")]
[ApiController]
public class SupplierController(ISupplierService supplierService)
    : ControllerBase
{
    [HttpGet("getAll")]
    public IActionResult GetAllSupplier()
    {
        return new Response(supplierService.GetAllSupplier());
    }

    [HttpGet("")]
    public IActionResult GetSupplierById([FromQuery(Name = "id")] int id)
    {
        return supplierService.GetSupplierById(id).Match(
            Some: result => new Response(result),
            None: () => new Response(
                null,
                statusCode: StatusCodes.Status500InternalServerError,
                message: AppContext.GetCodeMessage("opcs.error.supplier.not_exist")
            )
        );
    }
}