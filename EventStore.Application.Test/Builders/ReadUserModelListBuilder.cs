using EventStore.Application.Features.User;
using EventStore.Shared.Test;
using System.Collections.Generic;

namespace EventStore.Application.Test.Builders
{
    public class ReadUserModelListBuilder : GenericBuilder<List<ReadUserModel>>
    {
        public ReadUserModelListBuilder()
        {
            SetDefaults(() => new List<ReadUserModel>
            {
                ReadUserModel.CreateNewReadUser(
                    "aggregaterootid",
                    "oliver",
                    "verplancke",
                    "overplan",
                    0
                ),
                ReadUserModel.CreateNewReadUser(
                    "aggregaterootid",
                    "charme",
                    "balbuena",
                    "charmeb",
                    1
                ),
            });
        }
    }
}