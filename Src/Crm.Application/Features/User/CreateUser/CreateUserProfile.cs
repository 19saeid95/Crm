using AutoMapper;
using Crm.Application.Features.User.CreateUser;
using UserEntity = Crm.Domain.Entities.User;

namespace Crm.Application.Features.Users.CreateUser;

public sealed class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, UserEntity>()
            .ForMember(
                x => x.Id,
                opt => opt.Ignore())
            .ForMember(
                x => x.PasswordHash,
                opt => opt.Ignore())
            .ForMember(
                x => x.CreateDate,
                opt => opt.Ignore())
            .ForMember(
                x => x.LastUpdate,
                opt => opt.Ignore())
            .ForMember(
                x => x.IsDeleted,
                opt => opt.Ignore())
            .ForMember(
                x => x.UserRoles,
                opt => opt.Ignore());
    }
}