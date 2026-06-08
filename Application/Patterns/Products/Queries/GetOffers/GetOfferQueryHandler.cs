using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Products.Queries.GetOffers;

public class GetOfferQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOfferQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetOfferQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.GenericRepository<Product>().TableNoTracking.OrderDescending().Take(5)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}