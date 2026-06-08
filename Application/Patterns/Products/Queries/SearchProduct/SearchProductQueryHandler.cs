using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Products.Queries.SearchProduct;

public class SearchProductQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<SearchProductQuery, List<Product>>
{
    public async Task<List<Product>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.GenericRepository<Product>().TableNoTracking
            .Include(x => x.Category)
            .AsQueryable();
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(x =>
                x.Name.Contains(request.Search) ||
                x.Code.Contains(request.Search) ||
                x.Desc.Contains(request.Search));
        }

        return await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
    }
}