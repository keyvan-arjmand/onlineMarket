using System.Diagnostics;
using Application.Interfaces;
using Application.Patterns.Categories.Queries.GetAllCategory;
using Application.Patterns.Products.Queries.GetOffers;
using Application.Patterns.Products.Queries.GetProductById;
using Application.Patterns.Products.Queries.GetProducts;
using Application.Patterns.Products.Queries.GetProductsByCategoryId;
using Application.Patterns.Products.Queries.SearchProduct;
using Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ui.Models;

namespace Ui.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Offers = await _mediator.Send(new GetOfferQuery());
        ViewBag.prods = await _mediator.Send(new GetProductQuery());
        return View();
    }

    public async Task<IActionResult> Product(int id)
    {
        ViewBag.Offers = await _mediator.Send(new GetOfferQuery());
        ViewBag.prod = await _mediator.Send(new GetProductByIdQuery
        {
            Id = id
        });
        return View();
    }

    public async Task<IActionResult> Category(int id)
    {
        ViewBag.Cats = await _mediator.Send(new GetAllCategoryQuery());
        ViewBag.Prods = await _mediator.Send(new GetProductsByCategoryIdQuery
        {
            Id = id
        });
        return View();
    }

    public async Task<IActionResult> Search(string search)
    {
        ViewBag.Cats = await _mediator.Send(new GetAllCategoryQuery());
        ViewBag.Prods = await _mediator.Send(new SearchProductQuery
        {
            Search = search
        });
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}