using System.Threading.Tasks;
using EventStore.Application.Features.User.Register;
using EventStore.Application.Features.User.UpdateReadUser;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.User;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Register
{
    [TestClass]
    public class RegisterUserMediatorCommandHandlerTest
    {
        private IUserRepository _repository;
        private IMediatorFactory _mediatorFactory;

        private RegisterUserMediatorCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _repository = A.Fake<IUserRepository>();
            _mediatorFactory = A.Fake<IMediatorFactory>();

            _sut = new RegisterUserMediatorCommandHandler(_repository, _mediatorFactory);
        }

        [TestMethod]
        public async Task RegisterUser_Success()
        {
            // Arrange
            var request = RegisterUserMediatorCommand.CreateCommand("overplan", "olivier", "verplancke");
            
            var fakeScope = A.Fake<IMediatorScope>();
            A.CallTo(() => _mediatorFactory.CreateScope()).Returns(fakeScope);

            // Act
            await _sut.Handle(request, default);

            // Assert
            A.CallTo(() =>
                _repository.SaveUserAsync(A<Core.Domains.User.User>.That.Matches(u =>
                        u.FirstName == request.FirstName && u.LastName == request.LastName &&
                        u.UserName == request.UserName), default)).MustHaveHappenedOnceExactly();

            A.CallTo(() =>
                    fakeScope.SendAsync(A<UpdateReadUserCommand>.That.Matches(c =>
                            c.FirstName == request.FirstName && c.LastName == request.LastName && c.Version == 0),
                        default)).MustHaveHappenedOnceExactly();
        }
    }
}