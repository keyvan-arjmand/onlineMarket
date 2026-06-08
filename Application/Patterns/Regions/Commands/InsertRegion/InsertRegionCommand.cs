using MediatR;

namespace Application.Patterns.Regions.Commands.InsertRegion;

public record InsertRegionCommand(string Title, int? ParentId):IRequest;