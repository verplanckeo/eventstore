using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using EventStore.Application.Features.User;
using EventStore.Application.Features.User.Authenticate;
using EventStore.Application.Features.User.Password;
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
        private IBuilder<ValidateHashedPasswordMediatorQuery> _validateHashedPasswordMediatorQueryBuilder;
        private IBuilder<ValidateHashedPasswordMediatorQueryResult> _validateHashedPasswordMediatorQueryResultBuilder;
        private IBuilder<Core.Domains.User.User> _domainUserBuilder;
        private IBuilder<ReadUserModel> _readUserModelBuilder;

        private AuthenticateUserMediatorCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _readUserRepository = A.Fake<IReadUserRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _mediatorFactory = A.Fake<IMediatorFactory>();

            _authenticateUserMediatorCommandBuilder = new AuthenticateUserMediatorCommandBuilder();
            _authenticateUserMediatorCommandResponseBuilder = new AuthenticateUserMediatorCommandResponseBuilder();
            _validateHashedPasswordMediatorQueryBuilder = new ValidateHashedPasswordMediatorQueryBuilder();
            _validateHashedPasswordMediatorQueryResultBuilder = new ValidateHashedPasswordMediatorQueryResultBuilder();
            _domainUserBuilder = new DomainUserBuilder();
            _readUserModelBuilder = new ReadUserModelBuilder();

            _sut = new AuthenticateUserMediatorCommandHandler(_readUserRepository, _userRepository, _mediatorFactory);
        }

        [TestMethod]
        public async Task Given_A_User_Who_Authenticates__Success()
        {
            // Arrange
            var request = _authenticateUserMediatorCommandBuilder.Build();

            var validateRequest = _validateHashedPasswordMediatorQueryBuilder.Build();
            var validateResult = _validateHashedPasswordMediatorQueryResultBuilder.Build();
            var domainUser = _domainUserBuilder.Build();
            var readUserModel = _readUserModelBuilder.Build();

            var fakeScope = A.Fake<IMediatorScope>();
            A.CallTo(() => _mediatorFactory.CreateScope()).Returns(fakeScope);

            A.CallTo(() => _readUserRepository.LoadUserByUserNameAsync(request.UserName, default)).Returns(Task.FromResult(readUserModel));
            A.CallTo(() => _userRepository.LoadUserAsync(readUserModel.AggregateRootId, default)).Returns(Task.FromResult(domainUser));
            A.CallTo(() => fakeScope.SendAsync(A<ValidateHashedPasswordMediatorQuery>.Ignored, default)).Returns(Task.FromResult(validateResult));

            // Act
            var result = await _sut.Handle(request, default);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(readUserModel.AggregateRootId, result.Id);
            Assert.AreEqual("api-token", result.Token); //todo- this needs to be
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException))]
        public async Task Given_A_User_Who_Authenticates__Validation_Of_HashedPassword_Fails()
        {
            // Arrange
            var request = _authenticateUserMediatorCommandBuilder.Build();

            var validateRequest = _validateHashedPasswordMediatorQueryBuilder.Build();
            var validateResult = _validateHashedPasswordMediatorQueryResultBuilder
                .With(model => model.IsValid, false)
                .Build();
            var domainUser = _domainUserBuilder.Build();
            var readUserModel = _readUserModelBuilder.Build();

            var fakeScope = A.Fake<IMediatorScope>();
            A.CallTo(() => _mediatorFactory.CreateScope()).Returns(fakeScope);

            A.CallTo(() => _readUserRepository.LoadUserByUserNameAsync(request.UserName, default)).Returns(Task.FromResult(readUserModel));
            A.CallTo(() => _userRepository.LoadUserAsync(readUserModel.AggregateRootId, default)).Returns(Task.FromResult(domainUser));
            A.CallTo(() => fakeScope.SendAsync(A<ValidateHashedPasswordMediatorQuery>.Ignored, default)).Returns(Task.FromResult(validateResult));

            // Act
            var result = await _sut.Handle(request, default);

            // Assert (assertion is the expected exception on top of this test)
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task Given_A_User_Who_Authenticates__User_Not_Found()
        {
            // Arrange
            var request = _authenticateUserMediatorCommandBuilder.Build();

            A.CallTo(() => _readUserRepository.LoadUserByUserNameAsync(request.UserName, default)).Returns(Task.FromResult((ReadUserModel) null));

            // Act
            var result = await _sut.Handle(request, default);

            // Assert (assertion is the expected exception on top of this test)
        }
    }
}