using Microsoft.AspNetCore.Mvc;


namespace ManageMart;

public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        Console.WriteLine($"Category Name: {category.Name}");
        Console.WriteLine($"Category Description: {category.Description}");

        if (!ModelState.IsValid)
        {
            // Check for validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View("Index", category);
        }

        try
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Handle any errors that occurred during the save process
            Console.WriteLine(ex.Message);
            return View("Index", category);
        }
    }
}
