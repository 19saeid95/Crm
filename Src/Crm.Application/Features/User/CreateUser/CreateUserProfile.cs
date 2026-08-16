using AutoMapper;

namespace Crm.Application.Features.User.CreateUser;

public sealed class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, Crm.Domain.Entities.User>()
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