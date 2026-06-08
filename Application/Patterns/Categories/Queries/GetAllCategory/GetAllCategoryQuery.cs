using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Categories.Queries.GetAllCategory;

public class GetAllCategoryQuery:IRequest<List<Category>>
{
    public string? Search { get; set; }
}