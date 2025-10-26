using EventStore.Application.Features.User.Password;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class GetHashedPasswordMediatorQueryBuilder : GenericBuilder<GetHashedPasswordMediatorQuery>
    {
        public GetHashedPasswordMediatorQueryBuilder()
        {
            SetDefaults(() => GetHashedPasswordMediatorQuery.CreateQuery("securepassword"));
        }
    }
}