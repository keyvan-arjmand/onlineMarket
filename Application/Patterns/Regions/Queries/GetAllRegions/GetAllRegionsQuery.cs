using Domain.Entities.Regions;
using MediatR;

namespace Application.Patterns.Regions.Queries.GetAllRegions;

public class GetAllRegionsQuery:IRequest<List<Region>>
{
    public string? Search { get; set; }
}