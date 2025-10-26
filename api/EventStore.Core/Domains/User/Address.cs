using System.Collections.Generic;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User
{
    public class Address : ValueObject
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        //All these properties are used to check if an instance of address is equal to another instance of address
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return ZipCode;
            yield return Country;
        }
    }
}