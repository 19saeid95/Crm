using Crm.Application.Authorization;
using Crm.Application.Features.Locations.CreateLocation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.v1;

public class LocationsController(ISender sender) : BaseController
{
    [HttpPost("create")]
    [Authorize(Policy = Permissions.Location.Create)]
    public async Task<ActionResult<CreateLocationResponse>> Create(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return Created($"/api/v1/locations/{result.Id}", result);
    }
}