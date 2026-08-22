using AutoMapper;
using Crm.Application.Common.Exceptions;
using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(IUserRepository userRepository,ILocationRepository locationRepository, 
    ICustomerRepository customerRepository,IUnitOfWork unitOfWork,IMapper mapper)
    : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserIdAsync( request.UserId,cancellationToken);

        if (user is null)
            throw new NotFoundException("کاربر پیدا نشد.");

        var existingCustomerByUser = await customerRepository.GetByUserIdAsync( request.UserId, cancellationToken);

        if (existingCustomerByUser is not null)
            throw new ConflictException(  "این کاربر قبلاً به یک مشتری متصل شده است.");


        var location = await locationRepository.GetByIdAsync( request.LocationId,cancellationToken);

        if (location is null)
            throw new NotFoundException("موقعیت پیدا نشد.");

        if (!location.IsActive)
            throw new ConflictException( "موقعیت انتخاب‌شده غیرفعال است.");


        var existingCustomerByLocation = await customerRepository.GetByLocationIdAsync(request.LocationId, cancellationToken);

        if (existingCustomerByLocation is not null)
            throw new ConflictException( "این موقعیت قبلاً به یک مشتری متصل شده است.");


        var existingCustomerByCode = await customerRepository.GetByCustomerCodeAsync( request.CustomerCode, cancellationToken);

        if (existingCustomerByCode is not null)

            throw new ConflictException( "کد مشتری قبلاً استفاده شده است.");

        var customer = new Customer
        {
            UserId = request.UserId,
            LocationId = request.LocationId,
            CustomerCode = request.CustomerCode,
            IsActive = true,
            PurchasePerformanceScore = request.PurchasePerformanceScore,
            PurchaseMixQualityScore = request.PurchaseMixQualityScore,
            StoreCapacityScore = request.StoreCapacityScore,
            LoyaltyStrategicCooperationScore =request.LoyaltyStrategicCooperationScore,
            ProfessionalStaffQualityScore =request.ProfessionalStaffQualityScore,
            RegionalMarketPotentialScore = request.RegionalMarketPotentialScore
        };

        await customerRepository.AddAsync( customer, cancellationToken);

        await unitOfWork.SaveChangesAsync(  cancellationToken);

        return mapper.Map<CreateCustomerResponse>(customer);
    }
}