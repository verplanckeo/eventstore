using EventStore.Application.Features.User.Register;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class RegisterUserMediatorCommandBuilder : GenericBuilder<RegisterUserMediatorCommand>
    {
        public RegisterUserMediatorCommandBuilder()
        {
            SetDefaults(() =>
                RegisterUserMediatorCommand.CreateCommand(
                    "overplan",
                    "Olivier",
                    "Verplancke",
                    "securepassword"
                ));
        }
    }
}