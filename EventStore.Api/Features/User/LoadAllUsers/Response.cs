namespace EventStore.Api.Features.User.LoadAllUsers
{
    /// <summary>
    /// Response containing all users in our system.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// CTor
        /// </summary>
        private Response()
        {
            
        }

        /// <summary>
        /// Create new instance of <see cref="Response"/>
        /// </summary>
        /// <returns></returns>
        public static Response Create()
        {
            return new Response();
        }
    }
}