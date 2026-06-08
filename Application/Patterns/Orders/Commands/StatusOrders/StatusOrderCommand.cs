using MediatR;

namespace Application.Patterns.Orders.Commands.StatusOrders;

public record StatusOrderCommand(int Id, int Status) : IRequest;