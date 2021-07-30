using MediatR;

namespace EventStore.Application.Features.User.Password
{
    public class GetHashedPasswordMediatorQuery : IRequest<GetHashedPasswordMediatorQueryResult>
    {
        /// <summary>
        /// Original password entered by the user in the application
        /// </summary>
        public string Password { get; set; }

        //TODO: Rework default constructor - for now it's use only used for unit tests
        public GetHashedPasswordMediatorQuery() { }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="password"></param>
        private GetHashedPasswordMediatorQuery(string password)
        {
            Password = password;
        }

        /// <summary>
        /// Create an instance of the query
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static GetHashedPasswordMediatorQuery CreateQuery(string password)
        {
            return new GetHashedPasswordMediatorQuery(password);
        }
    }
}