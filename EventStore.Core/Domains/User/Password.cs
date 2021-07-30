using System.Collections.Generic;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User
{
    public class Password : ValueObject
    {
        public string PasswordValue { get; set; }

        public string Salt { get; set; }

        public Password(string password, string salt)
        {
            PasswordValue = password;
            Salt = salt;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PasswordValue;
            yield return Salt;
        }
    }
}