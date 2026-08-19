using MediatR;

namespace Crm.Application.Features.Authentication.LoginWithOtp;

public sealed record LoginWithOtpCommand(string Phone,string Code) : IRequest<LoginWithOtpResponse>;