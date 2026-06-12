namespace ECom.Core.Entites.Order
{
    public interface IShippingAddress
    {
        string City { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string State { get; set; }
        string Street { get; set; }
    }
}