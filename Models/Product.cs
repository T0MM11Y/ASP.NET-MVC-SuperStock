namespace ManageMart
{
  public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SellerId { get; set; }

        public Seller Seller { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string UrlPhoto { get; set; } 
        public int Stock { get; set; } 
        public int Price { get; set; } 
    }
}