using Domain.Common;

namespace Domain.Entities.Products;

public class Category:BaseEntity
{
    public string Title { get; set; }=string.Empty;
}