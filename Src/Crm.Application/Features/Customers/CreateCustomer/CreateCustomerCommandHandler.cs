using AutoMapper;
using Crm.Application.Common.Exceptions;
using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using Crm.Domain.Services;
using MediatR;

namespace Crm.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(IUserRepository userRepository, ILocationRepository locationRepository, ICustomerRepository customerRepository
    , IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByUserNameAsync(request.CustomerCode, cancellationToken);

        if (existingUser is not null)
            throw new ConflictException("کد مشتری قبلاً استفاده شده است.");


        var existingCustomer = await customerRepository.GetByCustomerCodeAsync(request.CustomerCode, cancellationToken);

        if (existingCustomer is not null)
            throw new ConflictException("کد مشتری قبلاً استفاده شده است.");


        var location = await locationRepository.GetByIdAsync(request.LocationId, cancellationToken);

        if (location is null)
            throw new NotFoundException("موقعیت پیدا نشد.");

        if (!location.IsActive)
            throw new ConflictException("موقعیت انتخاب‌شده غیرفعال است.");


        var user = new User
        {
            ParentUserId = null,
            Name = "مشتری",
            LastName = request.CustomerName,
            UserName = request.CustomerCode,
            PasswordHash = passwordHasher.Hash(request.CustomerCode),
            Phone = request.Phone,
            IsActive = true,
            IsSuperAdmin = false
        };

        await userRepository.AddAsync(user, cancellationToken);

        var customer = new Customer
        {
            User = user,
            CustomerName = request.CustomerName,
            CustomerCode = request.CustomerCode,
            LocationId = request.LocationId,
            IsActive = true,
            PurchasePerformanceScore = request.PurchasePerformanceScore,
            PurchaseMixQualityScore = request.PurchaseMixQualityScore,
            StoreCapacityScore = request.StoreCapacityScore,
            LoyaltyStrategicCooperationScore = request.LoyaltyStrategicCooperationScore,
            ProfessionalStaffQualityScore = request.ProfessionalStaffQualityScore,
            RegionalMarketPotentialScore = request.RegionalMarketPotentialScore
        };

        await customerRepository.AddAsync(customer, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CreateCustomerResponse>(customer);
    }
}