using EventStore.Application.Features.User;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace EventStore.Api.Features.User.LoadAllUsers
{
    /// <summary>
    /// Example of returned data when loading all users.
    /// </summary>
    public class ResponseExample : IExamplesProvider<Response>
    {
        /// <summary>
        /// Return an example for loading all users
        /// </summary>
        /// <returns></returns>
        public Response GetExamples()
        {
            return Response.Create(new List<ReadUserModel> { 
                ReadUserModel.CreateNewReadUser(Guid.NewGuid().ToString(), "Olivier", "Verplancke", "overplan", 1),
                ReadUserModel.CreateNewReadUser(Guid.NewGuid().ToString(), "Charme", "Balbuena", "charmeb", 2),
            });
        }
    }
}