using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")] public Category? Category { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}