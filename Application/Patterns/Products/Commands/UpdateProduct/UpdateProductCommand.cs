using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Patterns.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Code { get; set; }
    public string ImageUrl { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
}