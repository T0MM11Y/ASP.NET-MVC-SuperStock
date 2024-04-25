using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ManageMart;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AdminController(AppDbContext context, SignInManager<IdentityUser> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormFile sellerUrlPhoto, string sellerName,string sellerUsername, string sellerPhone, string sellerAddress)
    {
        var seller = new Seller
        {
            Name = sellerName,
            Username = sellerUsername.ToLower().Replace(" ", ""),
            Phone = sellerPhone,
            Address = sellerAddress,
            Password = "seller123",
            Role = "seller"
        };

        if (sellerUrlPhoto != null && sellerUrlPhoto.Length > 0)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/upload",
                sellerUrlPhoto.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await sellerUrlPhoto.CopyToAsync(stream);
            }

            // Normalize the image size
            using (Image image = Image.Load(path))
            {
                image.Mutate(x => x.Resize(500, 500)); // Change the size to whatever you want
                image.Save(path); // Save the new image
            }

            seller.UrlPhoto = "/upload/" + sellerUrlPhoto.FileName;
        }
        else
        {
            seller.UrlPhoto = "/image/no-image-available.png"; // Provide a default image path
        }

        _context.Add(seller);
        await _context.SaveChangesAsync();

        return RedirectToAction("Seller", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null)
        {
            return NotFound();
        }

        _context.Sellers.Remove(seller);
        await _context.SaveChangesAsync();

        return RedirectToAction("Seller", "Home");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null)
        {
            return NotFound();
        }

        return View(seller);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, IFormFile sellerUrlPhoto, Seller updatedSeller)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null)
        {
            return NotFound();
        }

        // Update the seller properties
        seller.Name = updatedSeller.Name;
        seller.Username = updatedSeller.Username;
        seller.Phone = updatedSeller.Phone;
        seller.Address = updatedSeller.Address;

        if (sellerUrlPhoto != null && sellerUrlPhoto.Length > 0)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/upload",
                sellerUrlPhoto.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await sellerUrlPhoto.CopyToAsync(stream);
            }

            // Normalize the image size
            using (Image image = Image.Load(path))
            {
                image.Mutate(x => x.Resize(500, 500)); // Change the size to whatever you want
                image.Save(path); // Save the new image
            }

            seller.UrlPhoto = "/upload/" + sellerUrlPhoto.FileName;
        }
        else
        {

            return RedirectToAction("Seller", "Home");
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Seller", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
    
  
}
