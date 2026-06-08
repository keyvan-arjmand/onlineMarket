namespace Ui.Models;

public class AddOrderDto
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int? RegionId { get; set; }

    public string? Desc { get; set; }
}