using System.Threading.Tasks;
using EventStore.Application.Features.User;
using EventStore.Application.Features.User.UpdateReadUser;
using EventStore.Application.Repositories.User;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.UpdateReadUser
{
    [TestClass]
    public class UpdateReadUserCommandHandlerTest
    {
        private IReadUserRepository _repository; 
        
        private UpdateReadUserCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _repository = A.Fake<IReadUserRepository>();

            _sut = new UpdateReadUserCommandHandler(_repository);
        }

        [TestMethod]
        public async Task UpdateReadUser_Success()
        {
            // Arrange
            var request = UpdateReadUserCommand.CreateCommand("root", "olivier", "verplancke", 0);

            // Act
            await _sut.Handle(request, default);

            // Assert
            A.CallTo(() =>
                _repository.SaveOrUpdateUserAsync(A<ReadUserModel>.That.Matches(u =>
                    u.FirstName == request.FirstName && u.LastName == request.LastName &&
                    u.AggregateRootId == request.AggregateRootId), default)).MustHaveHappenedOnceExactly();
        }
    }
}