using System.Collections.Generic;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User
{
    public class Password : ValueObject
    {
        public string HashedPassword { get; set; }

        public string Salt { get; set; }

        public Password(string hashedPassword, string salt)
        {
            HashedPassword = hashedPassword;
            Salt = salt;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return HashedPassword;
            yield return Salt;
        }
    }
}