using System;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User
{
    public class UserId : EntityId
    {
        private Guid _userId;

        public UserId()
        {
            _userId = Guid.NewGuid();
        }

        public UserId(string id)
        {
            if(!Guid.TryParse(id, out var userId)) throw new FormatException("Id of UserId EntityId should be a Guid (i.e. B71E987A-39BE-4707-B84A-E4667BD5C36E)");

            _userId = userId;
        }

        public override string ToString() => _userId.ToString();
    }
}