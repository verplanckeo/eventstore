using System.Threading.Tasks;
using EventStore.Core.Domains.User;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Infrastructure.Persistence.Test.Repositories
{
    [TestClass]
    public class EventStoreRepositoryTest
    {
        private IDatabaseContext _context;
        private EventStoreRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _context = new FakeDbContext();
            _sut = new EventStoreRepository(_context);
        }

        [TestMethod]
        public async Task Given_UserAggregateRegistered_When_SavedToEventStore_ShouldBeSameWhenFetched()
        {
            // Arrange
            var aggregateId = new UserId();
            var user = Core.Domains.User.User.CreateNewUser("overplan", "olivier", "verplancke", "demo");

            // Act
            await _sut.SaveAsync(aggregateId, user.Version, user.DomainEvents, "UserRegisteredAggregate", default);

            var events  = await _sut.LoadAsync(aggregateId, default);
            var fetchedUser = new Core.Domains.User.User(events);
            
            // Assert
            Assert.IsNotNull(events);
            Assert.AreEqual(fetchedUser, user); // equals check is overridden in ...\DddSeedwork\Entity.cs
        }
    }
}