using AutoMapper;
using Crm.Application.Common.Models;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Customers.GetCustomers;

public sealed class GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    : IRequestHandler<GetCustomersQuery, PaginatedResult<GetCustomersResponse>>
{
    public async Task<PaginatedResult<GetCustomersResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await customerRepository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

        var responses = mapper.Map<List<GetCustomersResponse>>(items);

        return new PaginatedResult<GetCustomersResponse>(
            responses,
            request.PageNumber,
            request.PageSize,
            totalCount);
    }
}