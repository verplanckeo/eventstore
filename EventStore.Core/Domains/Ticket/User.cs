using System;
using System.Collections.Generic;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.Ticket
{
    /// <summary>
    /// User linked to the registered ticket.
    /// </summary>
    public class User : ValueObject
    {
        /// <summary>
        /// Aggregate root id of the user linked to the ticket.
        /// </summary>
        public UserId UserId { get; private set; }

        /// <summary>
        /// Given username of the user linked to the ticket.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// DO NOT USER THIS CTOR - It is only here for unit tests
        /// </summary>
        public User()
        {
            
        }

        /// <summary>
        /// Create new instance of <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        public User(string userId, string userName)
        {
            UserId = new UserId(userId);
            UserName = userName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UserId;
            yield return UserName;
        }
    }
}