using System;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Password
{
    [TestClass]
    public class GetHashedPasswordMediatorQueryHandlerTest
    {
        private GetHashedPasswordMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new GetHashedPasswordMediatorQueryHandler();
        }

        [TestMethod]
        public async Task Given_A_Password_Hash_It__Success()
        {
            // Arrange
            var password = "securepassword";

            // Act
            var result = await _sut.Handle(GetHashedPasswordMediatorQuery.CreateQuery(password), default);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, string.IsNullOrEmpty(result.HashedPassword));
            Assert.AreEqual(false, string.IsNullOrEmpty(result.Salt));
            Assert.AreNotEqual(password, result.HashedPassword);

            Console.WriteLine($"Hashed password: {result.HashedPassword}");
            Console.WriteLine($"Salt: {result.Salt}");
        }
    }
}