using AutoMapper;
using Crm.Application.Common.Models;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Users.GetUsers;

public sealed class GetUsersQueryHandler(IUserRepository userRepository,IMapper mapper)
 : IRequestHandler<GetUsersQuery,PaginatedResult<GetUsersResponse>>
{
    public async Task<PaginatedResult<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var (users, totalCount) = await userRepository.GetPagedAsync( request.PageNumber,request.PageSize, cancellationToken);

        var items = mapper.Map<List<GetUsersResponse>>(users);

        return new PaginatedResult<GetUsersResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount);
    }
}