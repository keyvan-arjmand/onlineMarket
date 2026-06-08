using MediatR;

namespace Application.Patterns.Regions.Commands.UpdateRegion;

public record UpdateRegionCommand(int Id, string Title, int? ParentId) : IRequest;
