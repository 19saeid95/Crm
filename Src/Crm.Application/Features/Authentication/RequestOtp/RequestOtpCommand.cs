using MediatR;

namespace Crm.Application.Features.Authentication.RequestOtp;

public sealed record RequestOtpCommand(string Phone) : IRequest<RequestOtpResponse>;