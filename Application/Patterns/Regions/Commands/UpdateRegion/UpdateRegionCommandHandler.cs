using Application.Interfaces;
using Domain.Entities.Regions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Patterns.Regions.Commands.UpdateRegion;

public class UpdateRegionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRegionCommand>
{
    public async Task Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        var region = await unitOfWork
            .GenericRepository<Region>()
            .Table.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (region == null)
            return;

        region.Title = request.Title;
        region.ParentId = request.ParentId;

        await unitOfWork.GenericRepository<Region>().UpdateAsync(region, CancellationToken.None);
    }
}