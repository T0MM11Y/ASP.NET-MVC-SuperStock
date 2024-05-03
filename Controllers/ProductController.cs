using System.Net.Http.Json;
using System.Text.Json.Serialization;
using ManageMart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;


public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;


    public ProductController(AppDbContext context, IWebHostEnvironment hostEnvironment)
    {

        _context = context;
        _hostEnvironment = hostEnvironment;
    }
[HttpPost]
public async Task<IActionResult> Delete(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        return NotFound();
    }

    // Get the SellerId from the currently logged in seller
    var sellerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SellerId");
    if (sellerIdClaim != null && int.TryParse(sellerIdClaim.Value, out int sellerId))
    {
        // Check if the logged in seller is the same seller who added the product
        if (product.SellerId != sellerId)
        {
            // Handle error - the logged in seller is not the same seller who added the product
            TempData["ErrorMessage"] = "Anda tidak dapat menghapus produk ini karena bukan Anda yang menambahkannya";
            return RedirectToAction("Index");
        }
    }
    else
    {
        // Handle error - seller is not logged in or SellerId claim is not present
        return Unauthorized();
    }

    _context.Products.Remove(product);
    await _context.SaveChangesAsync();

    return RedirectToAction("Index");
}
    public IActionResult Index()
    {
        var viewModel = new ProductCategoryViewModel();

        viewModel.Categories = _context.Categories.ToList();
        viewModel.Products = _context.Products.Include(p => p.Seller).ToList() ?? new List<Product>(); // Ensure Products and their Sellers are initialized

        return View(viewModel);
    }



    [HttpPost]
    public async Task<IActionResult> Create(Product product, IFormFile UrlPhoto)
    {
        Console.WriteLine("Create method called");
        if (!ModelState.IsValid)
        {
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("Model is valid");
            if (UrlPhoto != null)
            {
                var fileName = Path.GetFileName(UrlPhoto.FileName);
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "products", fileName);

                using (var stream = new MemoryStream())
                {
                    await UrlPhoto.CopyToAsync(stream);
                    using (var image = SixLabors.ImageSharp.Image.Load(stream.ToArray()))
                    {
                        image.Mutate(x => x.Resize(180, 200)); // Change the size to whatever you need here
                        await image.SaveAsync(filePath);
                    }
                }

                product.UrlPhoto = "/products/" + fileName;
            }
            if (product.ExpiredAt == null)
            {
                product.ExpiredAt = DateTime.MaxValue; // Or any other default value
            }
            // Set SellerId from the currently logged in seller
            var sellerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SellerId");
            if (sellerIdClaim != null && int.TryParse(sellerIdClaim.Value, out int sellerId))
            {
                Console.WriteLine("SellerId claim found and parsed");
                product.SellerId = sellerId;
            }
            else
            {
                // Handle error - seller is not logged in or SellerId claim is not present
                Console.WriteLine("SellerId claim not found or could not be parsed");
                return Unauthorized();
            }
            try
            {
                _context.Add(product);
                product.CreatedAt = DateTime.Now.AddHours(7); // Set CreatedAt field with local time in Indonesia
                await _context.SaveChangesAsync();
                Console.WriteLine("Product saved to database");
            }
            catch (Exception ex)
            {
                // Log or print the exception message
                Console.WriteLine("Error saving product to database: " + ex.Message);

                // If there is an inner exception, print its message
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
            }
            return RedirectToAction("Index", "Product");
        }
        Console.WriteLine("Model is not valid");
        //index product
        var viewModel = new ProductCategoryViewModel();

        viewModel.Categories = _context.Categories.ToList();
        viewModel.Products = _context.Products.Include(p => p.Seller).ToList() ?? new List<Product>();
        return View("Index", viewModel);
    }
    [HttpGet]
    public async Task<IActionResult> FilterByCategory(int categoryId)
    {
        var products = await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    
        return Ok(products);
    }
    [HttpGet]
    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query is required");
        }

        var products = await _context.Products
            .Where(p => p.Name.Contains(query))
            .ToListAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product, IFormFile UrlPhoto)
    {
        Console.WriteLine("Create method called");
        if (!ModelState.IsValid)
        {
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("Model is valid");
            if (UrlPhoto != null)
            {
                var fileName = Path.GetFileName(UrlPhoto.FileName);
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "products", fileName);

                using (var stream = new MemoryStream())
                {
                    await UrlPhoto.CopyToAsync(stream);
                    using (var image = SixLabors.ImageSharp.Image.Load(stream.ToArray()))
                    {
                        image.Mutate(x => x.Resize(180, 160)); // Change the size to whatever you need here
                        await image.SaveAsync(filePath);
                    }
                }

                product.UrlPhoto = "/products/" + fileName;
            }

            if (UrlPhoto == null)
            {
                product.UrlPhoto = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id).UrlPhoto;
            }
            if (product.ExpiredAt == null)
            {
                product.ExpiredAt = DateTime.MaxValue; // Or any other default value
            }
            // Set SellerId from the currently logged in seller
            var sellerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SellerId");
            if (sellerIdClaim != null && int.TryParse(sellerIdClaim.Value, out int sellerId))
            {
                Console.WriteLine("SellerId claim found and parsed");
                product.SellerId = sellerId;
            }
            else
            {
                // Handle error - seller is not logged in or SellerId claim is not present
                Console.WriteLine("SellerId claim not found or could not be parsed");
                return Unauthorized();
            }
            var existingProduct = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                // Handle error - product not found
                Console.WriteLine("Product not found");
                return NotFound();
            }

            // Check if the logged in seller is the same seller who added the product
            if (existingProduct.SellerId != sellerId)
            {
                // Handle error - the logged in seller is not the same seller who added the product
                TempData["ErrorMessage"] = "Gak bisa ngedit bukan kamu yang menambahkan produk ini ";
                return View("Index", new ProductCategoryViewModel
                {
                    Categories = _context.Categories.ToList(),
                    Products = _context.Products.Include(p => p.Seller).ToList()
                });
            }

            try
            {
                _context.Update(product);


                await _context.SaveChangesAsync();
                Console.WriteLine("Product saved to database");
            }
            catch (Exception ex)
            {
                // Log or print the exception message
                Console.WriteLine("Error saving product to database: " + ex.Message);
            }
            return RedirectToAction("Index", "Product");

        }

        Console.WriteLine("Model is not valid");
        //index product
        var viewModel = new ProductCategoryViewModel();

        viewModel.Categories = _context.Categories.ToList();
        viewModel.Products = _context.Products.Include(p => p.Seller).ToList() ?? new List<Product>();
        return View("Index", viewModel);


    }
}
