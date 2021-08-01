using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using EventStore.Application.Services;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Password
{
    [TestClass]
    public class ValidateHashedPasswordMediatorQueryHandlerTest
    {
        private ISecurityService _securityService;

        private ValidateHashedPasswordMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _securityService = A.Fake<ISecurityService>();
            _sut = new ValidateHashedPasswordMediatorQueryHandler(_securityService);
        }

        [TestMethod]
        public async Task Given_A_Hashed_Password_Validation__Success()
        {
            // Arrange
            var hashedPassword = "hashed";
            var salt = "salt";
            var password = "securepassword";

            A.CallTo(() => _securityService.GenerateHashedPassword(password, salt)).Returns(Task.FromResult(hashedPassword));

            // Act
            var result = await _sut.Handle(ValidateHashedPasswordMediatorQuery.CreateQuery(password, hashedPassword, salt), default);

            // Assert
            Assert.AreEqual(true, result.IsValid);
        }

        [TestMethod]
        public async Task Given_A_Hashed_Password_Validation__Fail()
        {
            // Arrange
            var hashedPassword = "hashed";
            var salt = "salt";
            var password = "securepassword";

            A.CallTo(() => _securityService.GenerateHashedPassword(password, salt)).Returns(Task.FromResult("anotherhash"));

            // Act
            var result = await _sut.Handle(ValidateHashedPasswordMediatorQuery.CreateQuery(password, hashedPassword, salt), default);

            // Assert
            Assert.AreEqual(false, result.IsValid);
        }
    }
}