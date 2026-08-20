using AutoMapper;
using Crm.Application.Features.Users.CreateUser;
using Crm.Application.Features.Users.GetUserById;
using Crm.Application.Features.Users.GetUsers;
using Crm.Domain.Entities;

namespace Crm.Application.Common.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserResponse>();

        CreateMap<User, GetUserByIdResponse>();

        CreateMap<User, GetUsersResponse>();
    }
}