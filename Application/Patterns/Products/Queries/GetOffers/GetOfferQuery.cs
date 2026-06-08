using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Products.Queries.GetOffers;

public class GetOfferQuery:IRequest<List<Product>>
{
    
}