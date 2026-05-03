namespace WebTest.Models
{
    public class ProductInfo
    {
        public string Name { get; }
        public string Price { get; }

        public ProductInfo(string name, string price)
        {
            Name = name;
            Price = price;
        }
    }
}