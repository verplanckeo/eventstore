using EventStore.Application.Features.User;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class ReadUserModelBuilder : GenericBuilder<ReadUserModel>
    {
        public ReadUserModelBuilder()
        {
            SetDefaults(() => ReadUserModel.CreateNewReadUser(
                "aggregaterootid",
                "oliver",
                "verplancke",
                "overplan",
                1
            ));
        }
    }
}