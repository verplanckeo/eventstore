using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User.DomainEvents
{
    public class PasswordChangedDomainEvent : DomainEvent
    {
        /// <summary>
        /// Hashed password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt used to generate the hashed password.
        /// </summary>
        public string Salt { get; set; }

        public PasswordChangedDomainEvent(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}