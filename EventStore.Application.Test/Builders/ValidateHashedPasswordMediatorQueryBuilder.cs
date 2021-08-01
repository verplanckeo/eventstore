using EventStore.Application.Features.User.Password;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class ValidateHashedPasswordMediatorQueryBuilder : GenericBuilder<ValidateHashedPasswordMediatorQuery>
    {
        public ValidateHashedPasswordMediatorQueryBuilder()
        {
            SetDefaults(() => ValidateHashedPasswordMediatorQuery.CreateQuery("password", "hashedpassword", "salt"));
        }   
    }
}