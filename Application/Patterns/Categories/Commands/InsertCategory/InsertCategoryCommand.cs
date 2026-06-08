using MediatR;

namespace Application.Patterns.Categories.Commands.InsertCategory;

public class InsertCategoryCommand : IRequest
{
    public string Title { get; set; }
}