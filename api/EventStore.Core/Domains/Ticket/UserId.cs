using System;
using System.Collections.Generic;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.Ticket
{
    /*
     * Why have a copy of the same class, UserId, in a different domain?
     * The answer in my humble opinion: There should be no cross reference between domains.
     *   If, at some point in the future, you decide to move the User domain to a separate solution
     *   there will less issues moving away the code as there is no coupling whatsoever.
     *   Same with refactoring. Should you refactor the User domain, there is less of an impact, in this scenario, on the Ticket domain.
     *   The UserId in the User domain can get extra properties or change types ... But it doesn't impact the Ticket domain.
     */
    /// <summary>
    /// UserId reference in the Ticket domain.
    /// </summary>
    public class UserId : ValueObject
    {
        public Guid Id { get; }
        

        public UserId(string id)
        {
            if (!Guid.TryParse(id, out var userId)) throw new FormatException("Id of UserId should be a Guid (i.e. B71E987A-39BE-4707-B84A-E4667BD5C36E)");

            Id = userId;
        }

        public override string ToString() => Id.ToString("D");

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }
    }
}