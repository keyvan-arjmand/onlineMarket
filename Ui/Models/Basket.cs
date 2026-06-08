namespace Ui.Models;

public class Basket
{
    public long SumPrice { get; set; }
    public int DiscountAmount { get; set; }
    public string? DiscountCode { get; set; }
    public List<BasketItem> BasketItems { get; set; } = [];
}