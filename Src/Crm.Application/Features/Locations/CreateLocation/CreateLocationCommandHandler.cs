using AutoMapper;
using Crm.Application.Common.Exceptions;
using Crm.Domain.Entities;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Locations.CreateLocation;

public sealed class CreateLocationCommandHandler(ILocationRepository locationRepository,IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateLocationCommand, CreateLocationResponse>
{
    public async Task<CreateLocationResponse> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentLocationId.HasValue)
        {
            var parentLocation = await locationRepository.GetByIdAsync( request.ParentLocationId.Value,cancellationToken);

            if (parentLocation is null)
                throw new NotFoundException("موقعیت والد پیدا نشد.");

            if (!parentLocation.IsActive)
                throw new ConflictException("موقعیت والد غیرفعال است.");
        }

        var location = new Location
        {
            Name = request.Name,
            ParentLocationId = request.ParentLocationId,
            IsActive = true
        };

        await locationRepository.AddAsync(location,cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CreateLocationResponse>(location);
    }
}