using System.Linq;
using System.Threading.Tasks;
using EventStore.Application.Features.User;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Repositories.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Infrastructure.Persistence.Test.Repositories.User
{
    [TestClass]
    public class ReadUserRepositoryTest
    {
        private IDatabaseContext _context;
        private ReadUserRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _context = new FakeDbContext();
            _sut = new ReadUserRepository(_context);
        }

        [TestMethod]
        public async Task Given_ReadUserModelIsStored_Should_ReturnedSame_When_Fetched()
        {
            // Arrange
            var readModel = ReadUserModel.CreateNewReadUser("aggregateRootId", "olivier", "verplancke", "overplan", 0);

            // Act
            await _sut.SaveOrUpdateUserAsync(readModel, default);

            var fetchedReadModels = (await _sut.LoadUsersAsync(default)).ToList();
            
            // Assert
            Assert.IsNotNull(fetchedReadModels);
            Assert.AreEqual(readModel.AggregateRootId, fetchedReadModels[0].AggregateRootId);
            Assert.AreEqual(readModel.FirstName, fetchedReadModels[0].FirstName);
            Assert.AreEqual(readModel.LastName, fetchedReadModels[0].LastName);
            Assert.AreEqual(readModel.UserName, fetchedReadModels[0].UserName);
            Assert.AreEqual(1, fetchedReadModels[0].Version);
        }
    }
}