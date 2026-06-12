namespace ECom.Core.Entites.Order
{
    public class ShippingAddress: BaseEntity<int>
    {
        public ShippingAddress()
        {
            
        }

        public ShippingAddress(string firstName, string lastName, string street, string city, string state)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}