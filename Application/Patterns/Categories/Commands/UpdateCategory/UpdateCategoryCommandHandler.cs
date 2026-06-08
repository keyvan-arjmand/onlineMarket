using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = await unitOfWork.GenericRepository<Category>().Table.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (cat == null)
            return;

        cat.Title = request.Title;

        await unitOfWork.GenericRepository<Category>().UpdateAsync(cat, CancellationToken.None);
    }
}