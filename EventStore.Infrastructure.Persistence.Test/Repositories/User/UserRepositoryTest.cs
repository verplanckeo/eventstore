using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.User.DomainEvents;
using EventStore.Infrastructure.Persistence.Repositories.User;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Infrastructure.Persistence.Test.Repositories.User
{
    [TestClass]
    public class UserRepositoryTest
    {
        private IEventStoreRepository _store;
        private UserRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _store = A.Fake<IEventStoreRepository>();
            _sut = new UserRepository(_store);
        }

        [TestMethod]
        public async Task Given_UserAggregateRegistered_When_SavedToRepository_ShouldBeSameWhenFetched()
        {
            // Arrange
            var user = Core.Domains.User.User.CreateNewUser("overplan", "olivier", "verplancke");

            A.CallTo(() => _store.SaveAsync(user.Id, user.Version, user.DomainEvents, "UserAggregateRoot", default))
                .Returns(Task.CompletedTask);

            A.CallTo(() => _store.LoadAsync(user.Id, default)).Returns(
                new List<DomainEvent> {new UserRegisteredDomainEvent(user.Id.ToString(), user.UserName, user.FirstName, user.LastName)
            }.AsReadOnly());

            // Act
            await _sut.SaveUserAsync(user, default);
            var fetchedUser = await _sut.LoadUserAsync(user.Id.ToString(), default);

            // Assert
            Assert.IsNotNull(fetchedUser);
            Assert.AreEqual(user, fetchedUser);
        }
    }
}