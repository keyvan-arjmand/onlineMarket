using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Products.Queries.GetProducts;

public class GetProductQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.GenericRepository<Product>().TableNoTracking.Include(x => x.Category).OrderDescending()
            .ToListAsync(cancellationToken: cancellationToken);
    }
}