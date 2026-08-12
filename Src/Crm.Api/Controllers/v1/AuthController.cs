using Crm.Application.Features.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.V1;

public class AuthController(ISender sender) : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}