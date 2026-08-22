using Crm.Application.Common.Models;
using MediatR;

namespace Crm.Application.Features.Customers.GetCustomers;

public sealed record GetCustomersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedResult<GetCustomersResponse>>;