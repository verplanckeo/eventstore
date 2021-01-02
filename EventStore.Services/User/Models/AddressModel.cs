using EventStore.Core.Domains.User;

namespace EventStore.Services.User.Models
{
    public class AddressModel
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public static AddressModel FromDomain(Address address)
        {
            return new AddressModel
            {
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                Country = address.Country
            };
        }
    }
}