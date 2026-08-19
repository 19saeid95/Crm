using Crm.Application.Authorization;
using Crm.Application.Features.Authentication.Login;
using Crm.Application.Features.Authentication.RefreshToken;
using Crm.Application.Features.Users.CreateUser;
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
}

