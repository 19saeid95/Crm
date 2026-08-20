using Crm.Application.Common.Models;
using MediatR;

namespace Crm.Application.Features.Users.GetUsers;

public sealed record GetUsersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedResult<GetUsersResponse>>;