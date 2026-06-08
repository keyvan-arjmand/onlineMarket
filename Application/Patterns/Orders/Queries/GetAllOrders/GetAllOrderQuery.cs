using Domain.Entities.Orders;
using Domain.Enums;
using MediatR;

namespace Application.Patterns.Orders.Queries.GetAllOrders;

public class GetAllOrderQuery : IRequest<List<Order>>
{
    public string? Search { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}