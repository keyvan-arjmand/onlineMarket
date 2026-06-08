using Application.Interfaces;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Orders.Queries.GetAllOrders;

public class GetAllOrderQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrderQuery, List<Order>>
{
    public async Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork
            .GenericRepository<Order>()
            .TableNoTracking
            .Include(x => x.Customer)
            .Include(x => x.OrderItems).ThenInclude(x => x.Product)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x =>
                x.TrackingCode.Contains(request.Search) ||
                x.PhoneNumber.Contains(request.Search) ||
                x.Address.Contains(request.Search) ||
                x.Customer.UserName!.Contains(request.Search));
        }

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.OrderStatus == request.Status);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(x => x.OrderDate >= request.FromDate);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(x => x.OrderDate <= request.ToDate);
        }

        return await query
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync(cancellationToken);
    }
}