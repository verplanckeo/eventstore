using System.Threading.Tasks;
using EventStore.Application.Features.User.Authenticate;
using EventStore.Application.Test.Builders;
using EventStore.Shared.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Authenticate
{
    [TestClass]
    public class AuthenticateUserMediatorCommandValidatorTest
    {
        private AuthenticateUserMediatorCommandValidator _sut;
        private IBuilder<AuthenticateUserMediatorCommand> _builder;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new AuthenticateUserMediatorCommandValidator();
            _builder = new AuthenticateUserMediatorCommandBuilder();
        }

        [TestMethod]
        public async Task Given_A_User_Who_Wishes_To_Authenticate_Validation__Success()
        {
            // Arrange
            var authenticatingUser = _builder.Build();

            // Act
            var result = await _sut.ValidateAsync(authenticatingUser);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        [DataRow("", 1, false)]
        [DataRow("u", 1,  false)]
        [DataRow("us", 1, false)]
        [DataRow("user", 0, true)]
        public async Task Given_A_User_Without_UserName_Who_Wishes_To_Authenticate__Validation(string userName, int numberOfErrors, bool isValid)
        {
            // Arrange
            var authenticateUserCommand = _builder
                .With(command => command.UserName, userName)
                .Build();

            // Act
            var result = await _sut.ValidateAsync(authenticateUserCommand);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count, numberOfErrors);
        }
    }
}