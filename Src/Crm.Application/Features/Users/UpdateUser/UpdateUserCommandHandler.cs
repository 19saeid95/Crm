using AutoMapper;
using Crm.Application.Common.Exceptions;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Users.UpdateUser;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository,IUnitOfWork unitOfWork,IMapper mapper)
    : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle( UpdateUserCommand request,CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserIdAsync( request.Id,cancellationToken);

        if (user is null)
            throw new NotFoundException("کاربر پیدا نشد.");

        var existingUser = await userRepository.GetByUserNameAsync(request.UserName,cancellationToken);

        if (existingUser is not null &&existingUser.Id != request.Id)
            throw new ConflictException("نام کاربری قبلاً استفاده شده است.");


        if (request.ParentUserId.HasValue)
        {
            if (request.ParentUserId.Value == request.Id)
                throw new ConflictException( "کاربر نمی‌تواند والد خودش باشد.");

            var parentUser = await userRepository.GetByUserIdAsync( request.ParentUserId.Value,cancellationToken);

            if (parentUser is null)
                throw new NotFoundException("کاربر والد پیدا نشد.");

            if (!parentUser.IsActive)
                throw new ConflictException( "کاربر والد غیرفعال است.");
        }

        mapper.Map(request, user);
        user.LastUpdate = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UpdateUserResponse>(user);
    }
}