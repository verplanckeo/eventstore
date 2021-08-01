using EventStore.Application.Features.User.Password;
using EventStore.Application.Features.User.Register;
using EventStore.Application.Test.Builders;
using EventStore.Shared.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Password
{
    [TestClass]
    public class GetHashedPasswordMediatorQueryValidatorTest
    {
        private GetHashedPasswordMediatorQueryValidator _sut;
        private IBuilder<GetHashedPasswordMediatorQuery> _builder;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new GetHashedPasswordMediatorQueryValidator();
            _builder = new GetHashedPasswordMediatorQueryBuilder();
        }

        [TestMethod]
        public void Given_A_Password__Validation_Ok()
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
        [DataRow("s", 1, false)]
        [DataRow("se", 1, false)]
        [DataRow("sec", 1, false)]
        [DataRow("secu", 1, false)]
        [DataRow("secur", 1, false)]
        [DataRow("secure", 0, true)]
        public void Given_A_Query_Without_Password__Validation(string password, int numberOfErrors, bool isValid)
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