using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities.Orders
{
    public class OrderItem:BaseEntity
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")] public Order Order { get; set; } = default!;
        public int ProductId { get; set; }
        [ForeignKey("ProductId")] public Product Product { get; set; } = default!;
        public int Count { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
