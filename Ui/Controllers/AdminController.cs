using Application.Dtos;
using Application.Interfaces;
using Application.Patterns.Categories.Commands.InsertCategory;
using Application.Patterns.Categories.Commands.UpdateCategory;
using Application.Patterns.Categories.Queries.GetAllCategory;
using Application.Patterns.Orders.Commands.StatusOrders;
using Application.Patterns.Orders.Queries.GetAllOrders;
using Application.Patterns.Orders.Queries.GetOrderById;
using Application.Patterns.Products.Commands.InsertProduct;
using Application.Patterns.Products.Commands.UpdateProduct;
using Application.Patterns.Products.Queries.SearchProduct;
using Application.Patterns.Regions.Commands.InsertRegion;
using Application.Patterns.Regions.Commands.UpdateRegion;
using Application.Patterns.Regions.Queries.GetAllRegions;
using AutoMapper;
using Domain.Entities.Identities;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Regions;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ui.Models;

namespace Ui.Controllers;

[Authorize]
public class AdminController(
    IUnitOfWork unitOfWork,
    UserManager<User> _userManager,
    IFileService fileService,
    IMediator mediator,
    IMapper _mapper
)
    : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> OrderInfo(int id)
    {
        ViewBag.orderInfo = await mediator.Send(new GetOrderByIdQuery
        {
            Id = id
        });
        return View();
    }

    public async Task<IActionResult> Products(string? search)
    {
        ViewBag.prods = await mediator.Send(new SearchProductQuery
        {
            Search = search
        });
        ViewBag.Cats = await mediator.Send(new GetAllCategoryQuery());
        return View();
    }

    public async Task<IActionResult> Category(string? search)
    {
        ViewBag.cats = await mediator.Send(new GetAllCategoryQuery
        {
            Search = search
        });
        return View();
    }


    public async Task<IActionResult> Regions(string? search)
    {
        ViewBag.regions = await mediator.Send(new GetAllRegionsQuery
        {
            Search = search
        });
        return View();
    }

    public async Task<IActionResult> Orders(
        string? search,
        OrderStatus? status,
        DateTime? fromDate,
        DateTime? toDate)
    {
        ViewBag.Orders = await mediator.Send(new GetAllOrderQuery
        {
            Search = search,
            FromDate = fromDate,
            Status = status,
            ToDate = toDate
        });
        return View();
    }

    public async Task<IActionResult> Users(string? search)
    {
        var query = _userManager.Users.AsQueryable();

        // 🔍 Search by username / phone
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.UserName!.Contains(search) ||
                x.PhoneNumber!.Contains(search));
        }

        var users = await query
            .OrderByDescending(x => x.Id)
            .ToListAsync();

        ViewBag.Users = users;

        return View();
    }

    public async Task<IActionResult> UpdateProduct(ProductDto model)
    {
        await mediator.Send(_mapper.Map<UpdateProductCommand>(model));
        return RedirectToAction("Products");
    }

    public async Task<IActionResult> StatusOrder(int id, int status)
    {
        await mediator.Send(new StatusOrderCommand(id, status));
        return RedirectToAction("Orders");
    }

    public async Task<IActionResult> InsertProduct(InsertProductCommand model)
    {
        await mediator.Send(model);
        return RedirectToAction("Products");
    }

    public async Task<IActionResult> InsertCat(CatDto model)
    {
        await mediator.Send(new InsertCategoryCommand
        {
            Title = model.Title,
        });
        return RedirectToAction("Category");
    }

    public async Task<IActionResult> UpdateCat(CatDto model)
    {
        await mediator.Send(new UpdateCategoryCommand(model.Id, model.Title));
        return RedirectToAction("Category");
    }

    public async Task<IActionResult> UpdateUser(UpdateUser model)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString());

        if (user == null)
            return NotFound();

        user.UserName = model.Name;
        user.Email = model.Email;

        await _userManager.UpdateAsync(user);

        return RedirectToAction("Users");
    }

    public async Task<IActionResult> InsertRegion(string title, int? parentId)
    {
        await mediator.Send(new InsertRegionCommand(title, parentId));

        return RedirectToAction("Regions");
    }

    public async Task<IActionResult> UpdateRegion(int id, string title, int? parentId)
    {
        await mediator.Send(new UpdateRegionCommand(id, title, parentId));

        return RedirectToAction("Regions");
    }
}