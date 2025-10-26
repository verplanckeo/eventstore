using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.Application.Features.User.LoadAllUsers;
using EventStore.Application.Repositories.User;
using EventStore.Application.Test.Builders;
using EventStore.Shared.Test;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Features.User.Register
{
    [TestClass]
    public class LoadAllUsersMediatorCommandHandlerTest
    {
        private IReadUserRepository _repository;
        private IBuilder<List<ReadUserModel>> _readUsersBuilder;

        private LoadAllUsersMediatorCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _repository = A.Fake<IReadUserRepository>();
            _readUsersBuilder = new ReadUserModelListBuilder();

            _sut = new LoadAllUsersMediatorCommandHandler(_repository);
        }

        [TestMethod]
        public async Task LoadAllUsers_Success()
        {
            // Arrange
            var request = LoadAllUsersMediatorCommand.CreateCommand();
            var users = _readUsersBuilder.Build();
            A.CallTo(() => _repository.LoadUsersAsync(default)).Returns(users);

            // Act
            var result = await _sut.Handle(request, default);

            // Assert
            A.CallTo(() =>
                _repository.LoadUsersAsync(default)).MustHaveHappenedOnceExactly();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.AreEqual(2, result.Users.Count());
        }
    }
}