using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMart;

[Authorize(Roles = "seller")]
public class SellerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}