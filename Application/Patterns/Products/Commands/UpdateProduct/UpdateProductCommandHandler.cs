using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.GenericRepository<Product>().Table
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (product == null)
            return;

        product.Name = request.Name;
        product.Desc = request.Desc;
        product.Code = request.Code;
        product.CategoryId = request.CategoryId;
        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            product.ImageUrl = request.ImageUrl;
        }

        product.Price = request.Price;
        product.Quantity = request.Quantity;

        await unitOfWork.GenericRepository<Product>().UpdateAsync(product, CancellationToken.None);
    }
}