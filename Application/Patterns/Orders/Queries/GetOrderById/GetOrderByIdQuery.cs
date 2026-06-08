using Domain.Entities.Orders;
using MediatR;

namespace Application.Patterns.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery:IRequest<Order>
{
    public int Id { get; set; }
}