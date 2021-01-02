using EventStore.Application.Entities.User;

namespace EventStore.Services.User.Models
{
    /// <summary>
    /// Read model of user record
    /// </summary>
    public class ReadUserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static ReadUserModel FromEntity(ReadUser user)
        {
            return new ReadUserModel
            {
                Id = user.AggregateRootId,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}