using EventStore.Application.Features.User;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace EventStore.Api.Features.User.LoadSingleUser
{
    /// <summary>
    /// Example of returned data when loading a single user.
    /// </summary>
    public class ResponseExample : IExamplesProvider<Response>
    {
        /// <summary>
        /// Return an example for loading a single user.
        /// </summary>
        /// <returns></returns>
        public Response GetExamples()
        {
            return Response.Create(ReadUserModel.CreateNewReadUser(Guid.NewGuid().ToString(), "Olivier", "Verplancke", "overplan", 1));
        }
    }
}