namespace ECom.Core.Entites
{
    public class basketitem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? imageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}