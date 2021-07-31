using System.Threading.Tasks;
using EventStore.Application.Features.User.Authenticate;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.User;
using EventStore.Application.Test.Builders;
using EventStore.Shared.Test;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Authenticate
{
    [TestClass]
    public class AuthenticateUserMediatorCommandHandlerTest
    {
        private IReadUserRepository _readUserRepository;
        private IUserRepository _userRepository;
        private IMediatorFactory _mediatorFactory;

        private IBuilder<AuthenticateUserMediatorCommand> _authenticateUserMediatorCommandBuilder;
        private IBuilder<AuthenticateUserMediatorCommandResponse> _authenticateUserMediatorCommandResponseBuilder;

        private AuthenticateUserMediatorCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _readUserRepository = A.Fake<IReadUserRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _mediatorFactory = A.Fake<IMediatorFactory>();

            _authenticateUserMediatorCommandBuilder = new AuthenticateUserMediatorCommandBuilder();
            _authenticateUserMediatorCommandResponseBuilder = new AuthenticateUserMediatorCommandResponseBuilder();

            _sut = new AuthenticateUserMediatorCommandHandler(_readUserRepository, _userRepository, _mediatorFactory);
        }

        public async Task Given_A_User_Authenticate__Success()
        {
            // Arrange
            var model = _authenticateUserMediatorCommandBuilder.Build();
            var response = _authenticateUserMediatorCommandResponseBuilder.Build();

            var fakeScope = A.Fake<IMediatorScope>();
            A.CallTo(() => _mediatorFactory.CreateScope()).Returns(fakeScope);

            A.CallTo(() => fakeScope.SendAsync(A<AuthenticateUserMediatorCommand>.Ignored, default)).Returns(Task.FromResult(response));

            // Act


            // Assert



        }
    }
}