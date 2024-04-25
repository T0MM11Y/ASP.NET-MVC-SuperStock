namespace ManageMart;

public class Seller
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string UrlPhoto { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    


    public ICollection<Product> Products { get; set; }
}