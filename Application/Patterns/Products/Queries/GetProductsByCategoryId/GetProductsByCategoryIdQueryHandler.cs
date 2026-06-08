using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Products.Queries.GetProductsByCategoryId;

public class GetProductsByCategoryIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetProductsByCategoryIdQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.GenericRepository<Product>().TableNoTracking;
        if (request.Id > 0)
        {
            query = query.Where(x => x.CategoryId == request.Id);
        }

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
}