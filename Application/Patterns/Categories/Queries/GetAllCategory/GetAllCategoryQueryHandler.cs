using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Categories.Queries.GetAllCategory;

public class GetAllCategoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoryQuery, List<Category>>
{
    public async Task<List<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.GenericRepository<Category>().TableNoTracking.AsQueryable();

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