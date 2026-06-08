using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Products.Queries.GetProductsByCategoryId;

public class GetProductsByCategoryIdQuery:IRequest<List<Product>>
{
    public int Id { get; set; }
}