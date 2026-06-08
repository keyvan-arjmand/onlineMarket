using System.Dynamic;
using Application.Interfaces;
using Domain.Entities.Regions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ui.Views.Admin;

public class Regions : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Regions(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<Region> Region { get; set; } = [];

    public async Task OnGetAsync(string? search)
    {
        var query = _unitOfWork
            .GenericRepository<Region>()
            .TableNoTracking
            .Include(x => x.Parent)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Title.Contains(search));
        }

        Region = await query.ToListAsync();
    }

  

    public async Task<IActionResult> OnPostDeleteCatAsync(long id)
    {
        var region = await _unitOfWork
            .GenericRepository<Region>()
            .Table.FirstOrDefaultAsync(x => x.Id == id);


        if (region == null)
            return RedirectToPage();

        await _unitOfWork.GenericRepository<Region>().DeleteAsync(region, CancellationToken.None);

        return RedirectToPage();
    }
}