using Microsoft.AspNetCore.Http;

namespace Application.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Code { get; set; }
    public IFormFile ImageUrl { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
}