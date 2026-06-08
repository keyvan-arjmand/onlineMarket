using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Identities;

namespace Domain.Entities.Products;

public class ProductFav:BaseEntity
{
    public int? UserId { get; set; }
    [ForeignKey("UserId")] public User? User { get; set; } 
    public int? ProductId { get; set; }
    [ForeignKey("ProductId")] public Product? Product { get; set; } 
}