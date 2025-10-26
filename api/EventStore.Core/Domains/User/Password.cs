using System.Collections.Generic;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User
{
    public class Password : ValueObject
    {
        public string HashedPassword { get; private set; }

        public string Salt { get; private set; }
        
        //TODO: Rework default constructor - for now it's only used for unit test
        /// <summary>
        /// DO NOT USE THIS CTOR!
        /// </summary>
        public Password()
        {
            
        }

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