using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageMart;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewData["Error"] = "Username and password must not be empty.";
            return View();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        Seller seller = null;
        if (user == null)
        {
            seller = await _context.Sellers
                .FirstOrDefaultAsync(s => s.Username == username && s.Password == password);

            if (seller == null)
            {
                ViewData["Error"] = "Invalid username or password.";
                return View();
            }
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user != null ? user.Username : seller.Username),
        new Claim(ClaimTypes.Role, user != null ? user.Role : "seller"),
        new Claim("SellerId", seller != null ? seller.Id.ToString() : "")
    };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Redirect(user != null && user.Role == "admin" ? "/Admin/Index" : "/Product/Index");
    }
    [HttpPost]
    public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
    {
        if (newPassword != confirmPassword)
        {
            return Json(new { success = false, message = "New password and confirm password must be the same." });
        }
    
        var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Username == User.Identity.Name);
    
        if (seller == null || seller.Password != oldPassword)
        {
            return Json(new { success = false, message = "Old password is incorrect." });
        }
    
        seller.Password = newPassword;
        _context.Sellers.Update(seller);
        await _context.SaveChangesAsync();
    
        return Json(new { success = true, message = "Password has been changed successfully." });
    }
}