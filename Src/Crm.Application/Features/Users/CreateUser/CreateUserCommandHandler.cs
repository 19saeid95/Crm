
using Crm.Application.Common.Exceptions;
using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using Crm.Domain.Services;
using MediatR;

namespace Crm.Application.Features.Users.CreateUser;

public sealed class CreateUserCommandHandler(IUserRepository userRepository,IPasswordHasher passwordHasher
    ,IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByUserNameAsync(request.UserName, cancellationToken);

        if (existingUser is not null)
            throw new ConflictException("نام کاربری قبلاً استفاده شده است.");

        if (request.ParentUserId.HasValue)
        {
            var parentUser = await userRepository.GetByUserIdAsync(request.ParentUserId.Value, cancellationToken);

            if (parentUser is null)
                throw new NotFoundException("کاربر والد پیدا نشد.");


            if (!parentUser.IsActive)
                throw new ConflictException("کاربر والد غیرفعال است.");
        }

        var user = new User
        {
            UserName = request.UserName,
            PasswordHash = passwordHasher.Hash(request.Password),
            Phone = request.Phone,
            Name = request.Name,
            LastName = request.LastName,
            ParentUserId = request.ParentUserId,
            IsActive = true,
            IsSuperAdmin = false
        };

        await userRepository.AddAsync(user,cancellationToken);

        await unitOfWork.SaveChangesAsync( cancellationToken);


        return new CreateUserResponse(
            user.Id,
            user.UserName,
            user.Phone,
            user.Name,
            user.LastName,
            user.ParentUserId,
            user.IsActive);
    }
}
