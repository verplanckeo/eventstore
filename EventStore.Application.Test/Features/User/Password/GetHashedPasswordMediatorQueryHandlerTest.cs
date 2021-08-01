using System;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using EventStore.Application.Services;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.User.Password
{
    [TestClass]
    public class GetHashedPasswordMediatorQueryHandlerTest
    {
        private ISecurityService _securityService;
        
        private GetHashedPasswordMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _securityService = A.Fake<ISecurityService>();
            _sut = new GetHashedPasswordMediatorQueryHandler(_securityService);
        }

        [TestMethod]
        public async Task Given_A_Password_Hash_It__Success()
        {
            // Arrange
            var password = "securepassword";
            var hashed = "hashed";
            var salt = "salt";

            A.CallTo(() => _securityService.GenerateHashedPassword(password)).Returns((hashed, salt));

            // Act
            var result = await _sut.Handle(GetHashedPasswordMediatorQuery.CreateQuery(password), default);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hashed, result.HashedPassword);
            Assert.AreEqual(salt, result.Salt);
            Assert.AreNotEqual(password, result.HashedPassword);

            Console.WriteLine($"Hashed password: {result.HashedPassword}");
            Console.WriteLine($"Salt: {result.Salt}");
        }
    }
}