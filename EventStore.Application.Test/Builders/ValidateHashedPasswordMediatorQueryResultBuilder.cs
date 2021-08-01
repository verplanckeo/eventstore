using EventStore.Application.Features.User.Password;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class ValidateHashedPasswordMediatorQueryResultBuilder : GenericBuilder<ValidateHashedPasswordMediatorQueryResult>
    {
        public ValidateHashedPasswordMediatorQueryResultBuilder()
        {
            SetDefaults(() => ValidateHashedPasswordMediatorQueryResult.CreateResult(true));
        }
    }
}