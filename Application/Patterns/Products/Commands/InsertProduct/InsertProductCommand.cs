using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Patterns.Products.Commands.InsertProduct;

public class InsertProductCommand:IRequest
{
    public int? CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public IFormFile? ImageUrl { get; set; } 
    public int Price { get; set; }
    public int Quantity { get; set; }
}