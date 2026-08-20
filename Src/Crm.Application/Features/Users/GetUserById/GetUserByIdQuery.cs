using MediatR;

namespace Crm.Application.Features.Users.GetUserById;

public sealed record GetUserByIdQuery(long UserId) : IRequest<GetUserByIdResponse>;
