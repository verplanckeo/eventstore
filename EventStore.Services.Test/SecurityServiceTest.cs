using System;
using System.Threading.Tasks;
using EventStore.Infrastructure.Seedwork;
using EventStore.Services.Test.Builders;
using EventStore.Shared.Test;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Services.Test
{
    [TestClass]
    public class SecurityServiceTest
    {
        private SecurityService _sut;

        private IOptions<Jwt> _optionsJwt;

        private IBuilder<Jwt> _jwtBuilder;

        [TestInitialize]
        public void Initialize()
        {
            _optionsJwt = A.Fake<IOptions<Jwt>>();
            _jwtBuilder = new JwtBuilder();

            _sut = new SecurityService(_optionsJwt);
        }

        [TestMethod]
        public async Task Given_A_Password_Get_A_HashedPassword_With_Salt__Success()
        {
            // Arrange
            var password = "securepassword";
            var jwt = _jwtBuilder.Build();

            A.CallTo(() => _optionsJwt.Value).Returns(jwt);

            // Act
            (string hashedPassword, string salt) result = await _sut.GenerateHashedPassword(password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.hashedPassword));
            Assert.IsFalse(string.IsNullOrEmpty(result.salt));

            Console.WriteLine(result.hashedPassword);
            Console.WriteLine(result.salt);
        }

        [TestMethod]
        public async Task Given_A_Password_With_Salt_Get_SpecificHashed_Password__Success()
        {
            // Arrange
            var password = "securepassword";
            var hashed = "ECRNS9gLWtNf6hDWnw/ScSHjpLQLRsC783h5ZdZdP3I=";
            var salt = "ldP8jnoeovPrqWVJ7ZTWEA==";

            // Act
            var result = await _sut.GenerateHashedPassword(password, salt);

            // Assert
            Assert.AreEqual(hashed, result);
        }

        [TestMethod]
        public async Task Given_A_Password_With_Salt_Get_SpecificHashed_Password__Fail()
        {
            // Arrange
            var password = "securepassword";
            var hashed = "ECRNS9gLWtNf6hDWnw/ScSHjpLQLRsC783h5ZdZdP3I=";
            var salt = "jfI9jnoeovPrqWVJ7ZTWEA==";

            // Act
            var result = await _sut.GenerateHashedPassword(password, salt);

            // Assert
            Assert.AreNotEqual(hashed, result);
        }
    }
}