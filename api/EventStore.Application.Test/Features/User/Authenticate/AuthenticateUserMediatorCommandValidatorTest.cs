using System;
using EventStore.Application.Features.User.Authenticate;
using EventStore.Application.Test.Builders;
using EventStore.Shared.Test;
using FluentValidation.Results;
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
        public void Validate_A_Given_User_Who_Wishes_To_Authenticate__Success()
        {
            // Arrange
            var authenticatingUser = _builder.Build();

            // Act
            var result = _sut.Validate(authenticatingUser);

            // Assert
            Assert.IsTrue(result.IsValid);

            WriteErrorOutput(result);
        }

        [TestMethod]
        [DataRow(null, 3, false)]
        [DataRow("", 3, false)]
        [DataRow("u", 2,  false)]
        [DataRow("us", 0, true)]
        [DataRow("user", 0, true)]
        public void Validate_A_Given_User_Without_UserName_Who_Wishes_To_Authenticate__Validation(string userName, int numberOfErrors, bool isValid)
        {
            // Arrange
            var authenticateUserCommand = _builder
                .With(command => command.UserName, userName)
                .Build();

            // Act
            var result = _sut.Validate(authenticateUserCommand);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count, numberOfErrors);

            WriteErrorOutput(result);
        }

        [TestMethod]
        [DataRow(null, 3, false)]
        [DataRow("", 3, false)]
        [DataRow("s", 2,  false)]
        [DataRow("se", 3, false)]
        [DataRow("secure", 0, true)]
        public void Validate_A_Given_User_Without_Password_Who_Wishes_To_Authenticate__Validation(string password, int numberOfErrors, bool isValid)
        {
            // Arrange
            var authenticateUserCommand = _builder
                .With(command => command.Password, password)
                .Build();

            // Act
            var result = _sut.Validate(authenticateUserCommand);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count, numberOfErrors);
            
            WriteErrorOutput(result);
        }

        private void WriteErrorOutput(ValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
    }
}