using EventStore.Application.Features.User.Register;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class RegisterUserMediatorCommandBuilder : GenericBuilder<RegisterUserMediatorCommand>
    {
        private static readonly RegisterUserMediatorCommand Model = RegisterUserMediatorCommand.CreateCommand("overplan", "Olivier", "Verplancke", "securepassword");
        
        public RegisterUserMediatorCommandBuilder()
        {
            SetDefaults((model) =>
            {
                model.FirstName = Model.FirstName;
                model.LastName = Model.LastName;
                model.UserName = Model.UserName;
                model.Password = Model.Password;
            });
        }
    }
}