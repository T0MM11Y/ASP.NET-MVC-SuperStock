using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageMart;

[Authorize(Roles = "seller")]
public class SellerController : Controller
{
   

    public IActionResult Index()
    {
        return View();
    }

}