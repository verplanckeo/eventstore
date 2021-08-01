namespace EventStore.Api.Features.User.Authenticate
{
    /// <summary>
    /// Response returned when user is authenticated
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Identifier of the user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Access Token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        public Response(string id, string token)
        {
            Id = id;
            Token = token;
        }

        /// <summary>
        /// Return new instance of <see cref="Response"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Response CreateResponse(string id, string token) => new(id, token);
    }
}