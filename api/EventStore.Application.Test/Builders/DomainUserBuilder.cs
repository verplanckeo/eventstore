using EventStore.Core.Domains.User;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class DomainUserBuilder : GenericBuilder<User>
    {
        public DomainUserBuilder()
        {
            var user = User.CreateNewUser("overplan", "olivier", "verplancke");
            var password = new DomainUserPasswordBuilder().Build();

            user.ChangePassword(password.HashedPassword, password.Salt);
            SetDefaults(() => user);
        }
    }

    public class DomainUserPasswordBuilder : GenericBuilder<Password>
    {
        public DomainUserPasswordBuilder()
        {
            SetDefaults(() => new Password("hashed", "salt"));
        }
    }
}