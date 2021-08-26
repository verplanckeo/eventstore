namespace EventStore.Api.Features.User.LoadAllUsers
{
    /// <summary>
    /// Request to fetch all users in our system
    /// </summary>
    public class Request
    {
        /// <summary>
        /// CTor
        /// </summary>
        private Request()
        {

        }

        /// <summary>
        /// Create new instance of <see cref="Request"/>
        /// </summary>
        /// <returns></returns>
        public static Request Create()
        {
            return new Request();
        }
    }
}