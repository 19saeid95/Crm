using Crm.Application.Features.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.V1;

//[Authorize]
public class UserController(ISender sender) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> Create(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}