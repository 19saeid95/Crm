using AutoMapper;
using Crm.Application.Features.Customers.CreateCustomer;
using Crm.Application.Features.Customers.GetCustomers;
using Crm.Application.Features.Locations.CreateLocation;
using Crm.Application.Features.Users.CreateUser;
using Crm.Application.Features.Users.GetUserById;
using Crm.Application.Features.Users.GetUsers;
using Crm.Application.Features.Users.UpdateUser;
using Crm.Domain.Entities;

namespace Crm.Application.Common.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserResponse>();
        CreateMap<User, GetUserByIdResponse>();
        CreateMap<User, GetUsersResponse>();
        CreateMap<User, UpdateUserResponse>();
        CreateMap<Location, CreateLocationResponse>();
        CreateMap<Customer, CreateCustomerResponse>();
        CreateMap<UpdateUserCommand, User>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.PasswordHash, opt => opt.Ignore())
            .ForMember(x => x.IsActive, opt => opt.Ignore())
            .ForMember(x => x.IsSuperAdmin, opt => opt.Ignore())
            .ForMember(x => x.IsDeleted, opt => opt.Ignore())
            .ForMember(x => x.CreateDate, opt => opt.Ignore())
            .ForMember(x => x.LastUpdate, opt => opt.Ignore());

        CreateMap<Customer, GetCustomersResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name));


    }
}