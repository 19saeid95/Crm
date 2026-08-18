using Crm.Application.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.v1;

public class AuthController(ISender sender) : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }
}

