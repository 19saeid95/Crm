using Crm.Application.Features.Authentication.Login;
using Crm.Application.Features.Authentication.LoginWithOtp;
using Crm.Application.Features.Authentication.RefreshToken;
using Crm.Application.Features.Authentication.RequestOtp;
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

    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenResponse>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("request-otp")]
    public async Task<ActionResult<RequestOtpResponse>> RequestOtp([FromBody] RequestOtpCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login-otp")]
    public async Task<ActionResult<LoginWithOtpResponse>> LoginWithOtp([FromBody] LoginWithOtpCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }
}

