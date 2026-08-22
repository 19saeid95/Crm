using MediatR;

namespace Crm.Application.Features.Locations.CreateLocation;

public sealed record CreateLocationCommand(string Name, long? ParentLocationId) : IRequest<CreateLocationResponse>;