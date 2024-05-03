namespace ManageMart;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    // Relasi one-to-many dengan Product
    public ICollection<Product> Products { get; set; } = new List<Product>();
}