using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Products.Commands.InsertProduct;

public class InsertProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<InsertProductCommand>
{
    public async Task Handle(InsertProductCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.GenericRepository<Product>().AddAsync(mapper.Map<Product>(request), CancellationToken.None);
    }
}