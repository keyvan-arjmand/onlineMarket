using Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities.Regions;
using Domain.Enums;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))] public User Customer { get; set; } = default!;

        [MaxLength(500)] public string Desc { get; set; } = string.Empty;
        [MaxLength(50)] public string? DiscountCode { get; set; } 
        public int DiscountAmount { get; set; }

        [Required] [MaxLength(500)] public string Address { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public int? RegionId { get; set; }
        [ForeignKey("RegionId")] public Region? Region { get; set; }
        [Required] [MaxLength(20)] public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(50)] public string TrackingCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// مبلغ کل سفارش
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// هزینه ارسال
        /// </summary>
        public decimal ShippingPrice { get; set; }

        /// <summary>
        /// مبلغ نهایی
        /// </summary>
        public decimal FinalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// تاریخ ثبت سفارش
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.Now;

        /// <summary>
        /// تاریخ پرداخت
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// تاریخ ارسال
        /// </summary>
        public DateTime? ShippedDate { get; set; }

        /// <summary>
        /// تاریخ تحویل
        /// </summary>
        public DateTime? DeliveredDate { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; } = [];
    }
}