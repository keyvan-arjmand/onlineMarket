using System.Security.Claims;
using Application.Common.Utilities;
using Application.Interfaces;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ui.Models;

namespace Ui.Controllers;

public class BasketController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Basket()
    {
        var basket = HttpContext.Session.GetObject<Basket>("basket");

        if (basket == null)
        {
            basket = new Basket
            {
                BasketItems = new List<BasketItem>(),
                SumPrice = 0
            };
        }
        else
        {
            basket.SumPrice = basket.BasketItems.Sum(x => x.Price * x.Count);
        }

        ViewBag.Basket = basket;
        return View();
    }

    public async Task<IActionResult> Payment(int id)
    {
        ViewBag.Order = await unitOfWork.GenericRepository<Order>().TableNoTracking
            .Include(x => x.OrderItems).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        return View();
    }

    public IActionResult Checkout()
    {
        var basket = HttpContext.Session.GetObject<Basket>("basket");

        if (basket == null)
        {
            basket = new Basket
            {
                BasketItems = new List<BasketItem>(),
                SumPrice = 0
            };
        }
        else
        {
            basket.SumPrice = basket.BasketItems.Sum(x => x.Price * x.Count);
        }

        ViewBag.Basket = basket;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddDiscount(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return BadRequest("کد تخفیف وارد نشده است");

        var basket = HttpContext.Session.GetObject<Basket>("basket");

        if (basket == null || !basket.BasketItems.Any())
            return BadRequest("سبد خرید خالی است");

        var discount = await unitOfWork
            .GenericRepository<Discount>()
            .TableNoTracking
            .FirstOrDefaultAsync(x =>
                x.Code == code);

        if (discount == null)
            return BadRequest("کد تخفیف معتبر نیست");

        // جلوگیری از اعمال مجدد
        if (basket.DiscountCode == code)
            return BadRequest("این کد تخفیف قبلاً اعمال شده است");

        var totalPrice = basket.BasketItems.Sum(x => x.Price * x.Count);

        int discountAmount;

        discountAmount = discount.Amount;


        // تخفیف بیشتر از مبلغ سبد نشود
        if (discountAmount > totalPrice)
            discountAmount = totalPrice;

        basket.DiscountCode = discount.Code;
        basket.DiscountAmount = discountAmount;
        basket.SumPrice = totalPrice - discountAmount;

        HttpContext.Session.SetObject("basket", basket);

        return RedirectToAction("Basket", "Basket");
    }

    public async Task<IActionResult> AddOrder(AddOrderDto model)
    {
        // if (!ModelState.IsValid)
        //     return BadRequest("اطلاعات ناقص است");

        var basket = HttpContext.Session.GetObject<Basket>("basket");

        if (basket == null || !basket.BasketItems.Any())
            return BadRequest("سبد خرید خالی است");

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var totalPrice = basket.BasketItems.Sum(x => x.Price * x.Count);
        var shippingPrice = 50000; // بعداً داینامیک کن
        var finalPrice = totalPrice + shippingPrice;

        var order = new Order
        {
            UserId = userId,
            FullName = model.FullName,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            RegionId = model.RegionId,
            Desc = model.Desc ?? "",

            TrackingCode = $"ORD-{DateTime.Now.Ticks}",

            TotalPrice = totalPrice,
            ShippingPrice = shippingPrice,
            FinalPrice = finalPrice,

            OrderStatus = OrderStatus.PendingPayment,
            OrderDate = DateTime.Now,
            DiscountAmount = basket.DiscountAmount,
            DiscountCode = basket.DiscountCode
        };

        await unitOfWork.GenericRepository<Order>().AddAsync(order, CancellationToken.None);

        var orderItems = basket.BasketItems.Select(item => new OrderItem
        {
            OrderId = order.Id,
            ProductId = item.Id,
            Count = item.Count,
            Price = item.Price,
            TotalPrice = item.Price * item.Count
        }).ToList();
        await unitOfWork.GenericRepository<OrderItem>().AddRangeAsync(orderItems, CancellationToken.None);

        HttpContext.Session.Remove("basket");

        return RedirectToAction("Payment", "Basket", new {id = order.Id});
    }

    public async Task<IActionResult> AddToBasket(int id)
    {
        var product = await unitOfWork.GenericRepository<Product>().TableNoTracking
            .Select(x => new
            {
                x.Id,
                x.Price,
                x.Name,
                x.ImageUrl
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return NotFound();

        var basket = HttpContext.Session.GetObject<Basket>("basket") ?? new Basket();

        var item = basket.BasketItems.FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            item.Count += 1;
        }
        else
        {
            basket.BasketItems.Add(new BasketItem
            {
                Id = product.Id,
                Count = 1,
                Price = product.Price,
                Name = product.Name,
                ImageUrl = product.ImageUrl
            });
        }

        basket.SumPrice = basket.BasketItems.Sum(x => x.Price * x.Count);

        HttpContext.Session.SetObject("basket", basket);
        return RedirectToAction("Basket");
    }

    public async Task<IActionResult> ProductToBasket(int id)
    {
        var product = await unitOfWork.GenericRepository<Product>().TableNoTracking
            .Select(x => new
            {
                x.Id,
                x.Price,
                x.Name,
                x.ImageUrl
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return NotFound();

        var basket = HttpContext.Session.GetObject<Basket>("basket") ?? new Basket();

        var item = basket.BasketItems.FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            item.Count += 1;
        }
        else
        {
            basket.BasketItems.Add(new BasketItem
            {
                Id = product.Id,
                Count = 1,
                Price = product.Price,
                Name = product.Name,
                ImageUrl = product.ImageUrl
            });
        }

        basket.SumPrice = basket.BasketItems.Sum(x => x.Price * x.Count);

        HttpContext.Session.SetObject("basket", basket);
        return RedirectToAction("Product", "Home", new {id = product.Id});
    }

    public IActionResult GetBasket()
    {
        var basket = HttpContext.Session.GetObject<Basket>("basket");

        if (basket == null)
        {
            basket = new Basket
            {
                BasketItems = new List<BasketItem>(),
                SumPrice = 0
            };
        }
        else
        {
            // برای اطمینان از درست بودن جمع قیمت (recalculate)
            basket.SumPrice = basket.BasketItems.Sum(x => x.Price * x.Count);
        }

        return Ok(basket);
    }
}