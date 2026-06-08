using Application.Interfaces;
using Domain.Entities.Regions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Regions.Queries.GetAllRegions;

public class GetAllRegionsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRegionsQuery, List<Region>>
{
    public async Task<List<Region>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork
            .GenericRepository<Region>()
            .TableNoTracking.Include(x => x.Parent).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x =>
                x.Title.Contains(request.Search));
        }

        return await query
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}