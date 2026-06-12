namespace ECom.Core.Entites.Order
{
    public class OrderItem : BaseEntity<int>

    {
        public OrderItem()
        {
            
        }
        public OrderItem(int id ,string mainImage, string productName, decimal price, int quantity)
        {
            MainImage = mainImage;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
        public int ItemOrderedId { get; set; }
        public string MainImage { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}