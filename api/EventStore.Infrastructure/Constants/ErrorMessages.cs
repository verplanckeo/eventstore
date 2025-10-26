namespace EventStore.Infrastructure.Constants
{
    public class ErrorMessages
    {
        public class User
        {
            public const string ErrorUserNotAuthenticated = "ERR_USER_NOT_AUTHENTICATED";

            public const string ErrorUserNotFound = "ERR_USER_NOT_FOUND";

            public const string ErrorUserInvalidPassword = "ERR_USER_INVALID_PASSWORD";
        }

        public class Ticket
        {
            public const string TicketNotRegistered = "ERR_TICKET_NOT_REGISTERED";
        }
    }
}