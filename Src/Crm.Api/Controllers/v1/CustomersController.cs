using Crm.Api.Models;
using Crm.Application.Authorization;
using Crm.Application.Common.Models;
using Crm.Application.Features.Customers.CreateCustomer;
using Crm.Application.Features.Customers.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.v1;

public class CustomersController(ISender sender) : BaseController
{
    [HttpPost("create")]
    //[Authorize(Policy = Permissions.Customer.Create)]
    public async Task<ActionResult<CreateCustomerResponse>> Create( CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Created($"/api/v1/users/{result.Id}",ApiResponseFactory.Success( result,StatusCodes.Status201Created));
    }

    [HttpGet("get-all")]
    //[Authorize(Policy = Permissions.Customer.View)]
    public async Task<ActionResult<PaginatedResult<GetCustomersResponse>>> GetAll([FromQuery] GetCustomersQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send( query, cancellationToken);
        return Ok(ApiResponseFactory.Success(result));
    }
}
