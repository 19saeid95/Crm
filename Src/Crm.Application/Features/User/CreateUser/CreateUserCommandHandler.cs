using AutoMapper;
using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.User.CreateUser;

public sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<CreateUserResponse> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        // بررسی وجود کاربر
        var existingUser =
            await userRepository.GetByUserNameAsync(
                request.UserName,
                cancellationToken);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "این نام کاربری قبلاً ثبت شده است.");
        }

        var user = mapper.Map<Crm.Domain.Entities.User>(request);

        // مقداردهی دستی برخی فیلدها
        user.PasswordHash = passwordHasher.Hash(request.Password);
        user.CreateDate = DateTime.UtcNow;
        user.IsDeleted = false;

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse(user.Id, user.UserName);
    }
}