using MediatR;

namespace EventStore.Application.Features.User.Password
{
    public class ValidateHashedPasswordMediatorQuery : IRequest<ValidateHashedPasswordMediatorQueryResult>
    {
        /// <summary>
        /// Regular password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Hashed password
        /// </summary>
        public string HashedPassword { get; set; }

        /// <summary>
        /// Salt used to hash the password
        /// </summary>
        public string Salt { get; set; }

        //TODO: Rework default constructor - for now it's use only used for unit tests
        public ValidateHashedPasswordMediatorQuery() { }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="password"><see cref="Password"/></param>
        /// <param name="hashedPassword"><see cref="HashedPassword"/></param>
        /// <param name="salt"><see cref="Salt"/></param>
        private ValidateHashedPasswordMediatorQuery(string password, string hashedPassword, string salt)
        {
            Password = password;
            HashedPassword = hashedPassword;
            Salt = salt;
        }

        /// <summary>
        /// Create an instance of the query
        /// </summary>
        /// <param name="password"><see cref="Password"/></param>
        /// <param name="hashedPassword"><see cref="HashedPassword"/></param>
        /// <param name="salt"><see cref="Salt"/></param>
        /// <returns></returns>
        public static ValidateHashedPasswordMediatorQuery CreateQuery(string password, string hashedPassword, string salt)
        {
            return new ValidateHashedPasswordMediatorQuery(password, hashedPassword, salt);
        }
    }
}