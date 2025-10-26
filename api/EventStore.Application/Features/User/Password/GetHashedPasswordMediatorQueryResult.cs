namespace EventStore.Application.Features.User.Password
{
    /// <summary>
    /// Result returned when hashing password
    /// </summary>
    public class GetHashedPasswordMediatorQueryResult
    {
        /// <summary>
        /// Hashed password
        /// </summary>
        public string HashedPassword { get; private set; }

        /// <summary>
        /// Salt used to hash the password
        /// </summary>
        public string Salt { get; private set; }

        /// <summary>
        /// CTor
        /// </summary>
        public GetHashedPasswordMediatorQueryResult()
        {
            
        }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="hashedPassword"><see cref="HashedPassword"/></param>
        /// <param name="salt"><see cref="Salt"/></param>
        public GetHashedPasswordMediatorQueryResult(string hashedPassword, string salt)
        {
            HashedPassword = hashedPassword;
            Salt = salt;
        }

        /// <summary>
        /// Create instance of <see cref="GetHashedPasswordMediatorQueryResult"/>
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static GetHashedPasswordMediatorQueryResult CreateResult(string hashedPassword, string salt)
        {
            return new GetHashedPasswordMediatorQueryResult(hashedPassword, salt);
        }
    }
}