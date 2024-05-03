using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        return View(_context.Categories.ToList());
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
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Search(string term)
    {
        Console.WriteLine($"Searching for categories with name or description containing: {term}");

        List<Category> categories;
        if (string.IsNullOrEmpty(term))
        {
            // If the search term is empty, return all categories
            categories = await _context.Categories.ToListAsync();
        }
        else
        {
            categories = await _context.Categories
                .Where(c => c.Name.Contains(term) || c.Description.Contains(term))
                .ToListAsync();
        }

        Console.WriteLine($"{categories.Count} categories found");

        return Json(categories);
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Json(category);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        if (!CategoryExists(category.Id))
        {
            Console.WriteLine($"Category with ID {category.Id} does not exist");
            return NotFound();
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine($"Concurrency issue: {ex.Message}");
            return Conflict();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return StatusCode(500);
        }

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }

}