using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Products.Queries.SearchProduct;

public class SearchProductQuery:IRequest<List<Product>>
{
    public string? Search { get; set; }
}