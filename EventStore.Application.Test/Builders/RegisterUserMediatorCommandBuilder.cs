using EventStore.Application.Features.User.Register;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class RegisterUserMediatorCommandBuilder : GenericBuilder<RegisterUserMediatorCommand>
    {
        private RegisterUserMediatorCommand _model = RegisterUserMediatorCommand.CreateCommand("overplan", "Olivier", "Verplancke", "securepassword");
        
        public RegisterUserMediatorCommandBuilder()
        {
            SetDefaults((model) =>
            {
                model.FirstName = _model.FirstName;
                model.LastName = _model.LastName;
                model.UserName = _model.UserName;
                model.Password = _model.Password;
            });
        }
    }
}