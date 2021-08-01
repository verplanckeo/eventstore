using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Password
{
    [TestClass]
    public class ValidateHashedPasswordMediatorQueryHandlerTest
    {
        private ValidateHashedPasswordMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new ValidateHashedPasswordMediatorQueryHandler();
        }

        [TestMethod]
        [DataRow("ECRNS9gLWtNf6hDWnw/ScSHjpLQLRsC783h5ZdZdP3I=", "ldP8jnoeovPrqWVJ7ZTWEA==", true)]
        [DataRow("ECRNS9gLWtNf6hDWnw/ScSHjpLQLRsC783h5ZdZdP3I=", "jfI9jnoeovPrqWVJ7ZTWEA==", false)]
        public async Task Given_A_Hashed_Password__Validation(string hashedPassword, string salt, bool isValid)
        {
            // Arrange
            var password = "securepassword";

            // Act
            var result = await _sut.Handle(ValidateHashedPasswordMediatorQuery.CreateQuery(password, hashedPassword, salt), default);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);
        }
    }
}