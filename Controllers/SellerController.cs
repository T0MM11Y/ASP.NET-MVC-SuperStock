using ManageMart.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageMart;

[Authorize(Roles = "seller")]

public class SellerController : Controller
{
        private readonly SignInManager<IdentityUser> _signInManager;

    public SellerController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }
   

    public IActionResult Index()
    {
        return View();
    }

       [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}