using Crm.Application.Authorization;
using Crm.Application.Features.Users.CreateUser;
using Crm.Application.Features.Users.GetUserById;
using Crm.Application.Features.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.v1;

public class UsersController(ISender sender) : BaseController
{
    [HttpPost]
    [Authorize(Policy = Permissions.User.Create)]
    public async Task<ActionResult<CreateUserResponse>> Create(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return Created($"/api/v1/users/{result.Id}", result);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = Permissions.User.View)]
    public async Task<ActionResult<GetUserByIdResponse>> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.User.View)]
    public async Task<ActionResult<GetUsersResponse>> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}

