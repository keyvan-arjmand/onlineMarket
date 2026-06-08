using Application.Interfaces;
using Domain.Entities.Orders;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Orders.Commands.StatusOrders;

public class StatusOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<StatusOrderCommand>
{
    public async Task Handle(StatusOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.GenericRepository<Order>().Table.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (order == null)
            return;
        order.OrderStatus = (OrderStatus) request.Status;
        await unitOfWork.GenericRepository<Order>().UpdateAsync(order, CancellationToken.None);
    }
}