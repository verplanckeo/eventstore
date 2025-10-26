using System.Linq;
using EventStore.Core.Domains.User.DomainEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Core.Test.Domains.User
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Initialize()
        {

        }

        [TestMethod]
        public void CreateNewUser_Ok()
        {
            // Arrange

            // Act
            var user = Core.Domains.User.User.CreateNewUser("overplan", "olivier", "verplancke");

            // Assert
            Assert.AreEqual("overplan", user.UserName);
            Assert.AreEqual("olivier", user.FirstName);
            Assert.AreEqual("verplancke", user.LastName);
            Assert.AreEqual(1, user.DomainEvents.Count);
            Assert.AreEqual(typeof(UserRegisteredDomainEvent), user.DomainEvents.First().GetType());
        }

        [TestMethod]
        public void ChangeAddress_Ok()
        {
            // Arrange
            var user = Core.Domains.User.User.CreateNewUser("overplan", "olivier", "verplancke");

            // Act
            user.ChangeAddress("street", "city", "1", "belgium");

            // Assert
            Assert.AreEqual("street", user.UserAddress.Street);
            Assert.AreEqual("city", user.UserAddress.City);
            Assert.AreEqual("1", user.UserAddress.ZipCode);
            Assert.AreEqual("belgium", user.UserAddress.Country);
            Assert.AreEqual(2, user.DomainEvents.Count);
            Assert.AreEqual(typeof(AddressChangedDomainEvent), user.DomainEvents.ToList()[1].GetType());
        }
    }
}