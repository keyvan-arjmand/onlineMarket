using Domain.Entities.Products;
using Domain.Migrations;
using MediatR;

namespace Application.Patterns.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
}