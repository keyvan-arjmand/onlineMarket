using Application.Interfaces;
using Domain.Entities.Regions;
using MediatR;

namespace Application.Patterns.Regions.Commands.InsertRegion;

public class InsertRegionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<InsertRegionCommand>
{
    public async Task Handle(InsertRegionCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.GenericRepository<Region>().AddAsync(new Region
        {
            Title = request.Title,
            ParentId = request.ParentId,
        }, CancellationToken.None);
    }
}