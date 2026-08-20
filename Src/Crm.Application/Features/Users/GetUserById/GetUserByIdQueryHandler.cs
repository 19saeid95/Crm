using AutoMapper;
using Crm.Application.Common.Exceptions;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Users.GetUserById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository,IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetDetailsByUserIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("کاربر پیدا نشد.");

        return mapper.Map<GetUserByIdResponse>(user);
    }
}
