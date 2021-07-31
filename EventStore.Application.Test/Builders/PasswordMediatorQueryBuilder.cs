using EventStore.Application.Features.User.Password;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class PasswordMediatorQueryBuilder : GenericBuilder<GetHashedPasswordMediatorQuery>
    {
        private static readonly GetHashedPasswordMediatorQuery Model = GetHashedPasswordMediatorQuery.CreateQuery("securepassword");

        public PasswordMediatorQueryBuilder()
        {
            SetDefaults(model =>
            {
                model.Password = Model.Password;
            });
        }
    }
}