using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ui.Controllers;

public class AuthController : Controller
{
     private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    // 🔥 Auto Login/Register
    [HttpPost]
    public async Task<IActionResult> Login(string phoneNumber, string password)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "شماره و رمز الزامی است");
            return View();
        }

        // 1. پیدا کردن کاربر
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        // 2. اگر نبود => ساخت کاربر
        if (user == null)
        {
            user = new User
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
            };

            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                    ModelState.AddModelError("", error.Description);

                return View();
            }
        }
        else
        {
            // 3. اگر بود => چک پسورد
            var checkPassword = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure: false);

            if (!checkPassword.Succeeded)
            {
                ModelState.AddModelError("", "رمز اشتباه است");
                return View();
            }
        }

        // 4. لاگین نهایی
        await _signInManager.SignInAsync(user, isPersistent: true);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Register");
    }
}