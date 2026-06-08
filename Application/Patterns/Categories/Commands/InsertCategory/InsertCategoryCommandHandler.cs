using Application.Interfaces;
using Domain.Entities.Products;
using MediatR;

namespace Application.Patterns.Categories.Commands.InsertCategory;

public class InsertCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<InsertCategoryCommand>
{
    public async Task Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.GenericRepository<Category>().AddAsync(new Category
        {
            Title = request.Title,
        }, CancellationToken.None);
    }
}