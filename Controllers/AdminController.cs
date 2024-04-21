using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMart;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}