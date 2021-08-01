namespace EventStore.Application.Features.User.Register
{
    public class RegisterUserMediatorCommandResponse
    {
        public string Id { get; private set; }

        private RegisterUserMediatorCommandResponse(string id)
        {
            Id = id;
        }

        public static RegisterUserMediatorCommandResponse CreateResponse(string id)
        {
            return new RegisterUserMediatorCommandResponse(id);
        }
    }
}