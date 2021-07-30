namespace EventStore.Application.Features.User.Password
{
    public class ValidateHashedPasswordMediatorQueryResult
    {
        /// <summary>
        /// Is password valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        public ValidateHashedPasswordMediatorQueryResult()
        {
            
        }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="isValid"></param>
        private ValidateHashedPasswordMediatorQueryResult(bool isValid)
        {
            IsValid = isValid;
        }

        /// <summary>
        /// Create an instance of <see cref="ValidateHashedPasswordMediatorQueryResult"/>
        /// </summary>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static ValidateHashedPasswordMediatorQueryResult CreateResult(bool isValid)
        {
            return new ValidateHashedPasswordMediatorQueryResult(isValid);
        }
    }
}