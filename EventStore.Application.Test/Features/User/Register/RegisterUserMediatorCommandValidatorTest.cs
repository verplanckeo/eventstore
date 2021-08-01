using EventStore.Application.Features.User.Register;
using EventStore.Application.Test.Builders;
using EventStore.Shared.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Register
{
    [TestClass]
    public class RegisterUserMediatorCommandValidatorTest
    {
        private RegisterUserMediatorCommandValidator _sut;
        private IBuilder<RegisterUserMediatorCommand> _builder;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new RegisterUserMediatorCommandValidator();
            _builder = new RegisterUserMediatorCommandBuilder();
        }

        [TestMethod]
        public void Given_A_User__Validation_Ok()
        {
             // Arrange
             var model = _builder.Build();

             // Act
             var result = _sut.Validate(model);
                
             // Assert
             Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        [DataRow(null, 2, false)]
        [DataRow("", 2, false)]
        [DataRow("o", 1, false)]
        [DataRow("ov", 0, true)]
        public void Given_A_User_Without_UserName__Validation(string userNameValue, int numberOfErrors, bool isValid)
        {

            // Arrange
            var model = _builder
                .With(command => command.UserName, userNameValue)
                .Build();

            // Act
            var result = _sut.Validate(model);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);
        }

        [TestMethod]
        [DataRow(null, 2, false)]
        [DataRow("", 2, false)]
        [DataRow("O", 1, false)]
        [DataRow("Ov", 0, true)]
        public void Given_A_User_Without_FirstName__Validation(string firstName, int numberOfErrors, bool isValid)
        {

            // Arrange
            var model = _builder
                .With(command => command.FirstName, firstName)
                .Build();

            // Act
            var result = _sut.Validate(model);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);
        }

        [TestMethod]
        [DataRow(null, 2, false)]
        [DataRow("", 2, false)]
        [DataRow("V", 1, false)]
        [DataRow("Ve", 0, true)]
        public void Given_A_User_Without_LastName__Validation(string password, int numberOfErrors, bool isValid)
        {

            // Arrange
            var model = _builder
                .With(command => command.LastName, password)
                .Build();

            // Act
            var result = _sut.Validate(model);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);
        }

        [TestMethod]
        [DataRow(null, 2, false)]
        [DataRow("", 2, false)]
        [DataRow("s", 1, false)]
        [DataRow("se", 1, false)]
        [DataRow("sec", 1, false)]
        [DataRow("secu", 1, false)]
        [DataRow("secur", 1, false)]
        [DataRow("secure", 0, true)]
        public void Given_A_User_Without_Password__Validation(string password, int numberOfErrors, bool isValid)
        {

            // Arrange
            var model = _builder
                .With(command => command.Password, password)
                .Build();

            // Act
            var result = _sut.Validate(model);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count, numberOfErrors);
        }
    }
}