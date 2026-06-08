using Application.Interfaces;
using Domain.Entities.Identities;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ui.Controllers;

[Authorize]
public class UserController(IUnitOfWork unitOfWork, UserManager<User> _userManager, IFileService fileService)
    : Controller
{
    // GET
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);

        ViewBag.CurrentUser = user;

        return View();
    }

    public async Task<IActionResult> Favorite()
    {
        var user = await _userManager.GetUserAsync(User);

        ViewBag.CurrentUser = user;
        ViewBag.favs = await unitOfWork.GenericRepository<ProductFav>().TableNoTracking
            .Include(x => x.Product)
            .Where(x => x.UserId == user.Id)
            .OrderDescending()
            .ToListAsync();

        return View();
    }

    public async Task<IActionResult> AddFav(int id)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return RedirectToAction("Login", "Auth");

        var exist = await unitOfWork
            .GenericRepository<ProductFav>()
            .TableNoTracking
            .AnyAsync(x => x.ProductId == id && x.UserId == user.Id);

        if (!exist)
        {
            await unitOfWork
                .GenericRepository<ProductFav>()
                .AddAsync(
                    new ProductFav
                    {
                        ProductId = id,
                        UserId = user.Id
                    },
                    CancellationToken.None);
        }

        return RedirectToAction("Product", "Home", new {id});
    }

    public async Task<IActionResult> Orders()
    {
        var user = await _userManager.GetUserAsync(User);

        ViewBag.CurrentUser = user;
        ViewBag.Orders = await unitOfWork.GenericRepository<Order>().TableNoTracking.Where(x => x.UserId == user.Id)
            .OrderDescending().ToListAsync();
        return View();
    }

    public async Task<IActionResult> OrderDetail(int id)
    {
        var user = await _userManager.GetUserAsync(User);

        ViewBag.CurrentUser = user;
        ViewBag.Order =
            await unitOfWork.GenericRepository<Order>().TableNoTracking.Include(x => x.OrderItems)
                .ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
        return View();
    }

    public async Task<IActionResult> Information()
    {
        var user = await _userManager.GetUserAsync(User);

        ViewBag.CurrentUser = user;

        return View();
    }

    public async Task<IActionResult> UpdateProfile(string name, string email, string family)
    {
        var user = await _userManager.GetUserAsync(User);
        user.Name = name;
        user.Email = email;
        user.Family = family;
        await _userManager.UpdateAsync(user);
        return RedirectToAction("Information");
    }
}