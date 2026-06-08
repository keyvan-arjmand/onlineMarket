using MediatR;

namespace Application.Patterns.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int Id, string Title) : IRequest;