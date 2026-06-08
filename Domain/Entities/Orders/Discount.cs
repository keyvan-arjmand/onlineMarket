using Domain.Common;

namespace Domain.Entities.Orders;

public class Discount : BaseEntity
{
    public int Amount { get; set; }
    public int Count { get; set; }
    public string Code { get; set; } = string.Empty;
}