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
    private readonly ILogger<AdminController> _logger;


    public AdminController(AppDbContext context, SignInManager<IdentityUser> signInManager, ILogger<AdminController> logger)
    {
        _context = context;
        _signInManager = signInManager;
        _logger = logger;
    }
    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            TotalProducts = _context.Products.Count(),
            TotalStock = _context.Products.Sum(p => p.Stock),
            TotalCategories = _context.Categories.Count(),
            TotalSellers = _context.Sellers.Count()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormFile sellerUrlPhoto, string sellerName, string sellerUsername, string sellerPhone, string sellerAddress)
    {
        var seller = new Seller
        {
            UserId = Guid.NewGuid().ToString(),
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
        _logger.LogInformation("Edit method started.");

        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null)
        {
            _logger.LogWarning("Seller with id {Id} not found.", id);
            return NotFound();
        }

        // Update the seller properties
        _logger.LogInformation("Updating seller properties.");
        seller.Name = updatedSeller.Name;
        seller.Username = updatedSeller.Username;
        seller.Phone = updatedSeller.Phone;
        seller.Address = updatedSeller.Address;


        if (sellerUrlPhoto != null && sellerUrlPhoto.Length > 0)
        {
            _logger.LogInformation("Processing seller photo.");
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/upload",
                sellerUrlPhoto.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await sellerUrlPhoto.CopyToAsync(stream);
            }

            // Normalize the image size
            _logger.LogInformation("Normalizing image size.");
            using (Image image = Image.Load(path))
            {
                image.Mutate(x => x.Resize(500, 500)); // Change the size to whatever you want
                image.Save(path); // Save the new image
            }

            seller.UrlPhoto = "/upload/" + sellerUrlPhoto.FileName;
        }
        if (sellerUrlPhoto == null)
        {
            //tetap kirim foto sebelumnya yang ada di database
            seller.UrlPhoto = seller.UrlPhoto;

        }


        _logger.LogInformation("Saving changes to database.");
        await _context.SaveChangesAsync();

        _logger.LogInformation("Edit method completed, redirecting to Seller page.");
        return RedirectToAction("Seller", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }


}
