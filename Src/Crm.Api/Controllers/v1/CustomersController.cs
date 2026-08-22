using Crm.Application.Authorization;
using Crm.Application.Features.Customers.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.v1;

public class CustomersController(ISender sender) : BaseController
{
    [HttpPost("create")]
    [Authorize(Policy = Permissions.Customer.Create)]
    public async Task<ActionResult<CreateCustomerResponse>> Create(
       CreateCustomerCommand command,
       CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Created($"/api/v1/customers/{result.Id}", result);
    }
}
