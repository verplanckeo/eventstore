using EventStore.Application.Features.User.Password;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class PasswordMediatorQueryBuilder : GenericBuilder<GetHashedPasswordMediatorQuery>
    {
        private GetHashedPasswordMediatorQuery _model => GetHashedPasswordMediatorQuery.CreateQuery("securepassword");

        public PasswordMediatorQueryBuilder()
        {
            SetDefaults(model =>
            {
                model.Password = _model.Password;
            });
        }
    }
}