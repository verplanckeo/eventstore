using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.User.LoadAllUsers

{
    /// <summary>
    /// Example of loading list of all users in our system.
    /// </summary>
    public class RequestExample : IExamplesProvider<Request>
    {
        /// <summary>
        /// Return an example request for loading all users.
        /// </summary>
        /// <returns></returns>
        public Request GetExamples()
        {
            return Request.Create();
        }
    }
}