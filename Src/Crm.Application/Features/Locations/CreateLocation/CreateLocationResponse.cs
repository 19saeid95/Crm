namespace Crm.Application.Features.Locations.CreateLocation;

public sealed record CreateLocationResponse(long Id, string Name, long? ParentLocationId, bool IsActive);