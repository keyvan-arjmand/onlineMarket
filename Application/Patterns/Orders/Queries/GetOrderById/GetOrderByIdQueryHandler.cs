using Application.Interfaces;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderByIdQuery, Order>
{
    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork
            .GenericRepository<Order>()
            .TableNoTracking
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .AsQueryable();
        return await query.FirstOrDefaultAsync(x => x.Id == request.Id);
    }
}