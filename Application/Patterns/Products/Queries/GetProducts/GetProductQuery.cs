using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Products.Queries.GetProducts;

public class GetProductQuery:IRequest<List<Product>>
{
    
}