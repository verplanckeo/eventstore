namespace EventStore.Services.User.Models
{
    /// <summary>
    /// Model representation of a user record
    /// </summary>
    public class UserModel
    {
        public string UserId { get; protected set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public AddressModel Address { get; private set; }

        public static UserModel FromDomain(Core.Domains.User.User user)
        {
            return new UserModel
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = AddressModel.FromDomain(user.UserAddress)
            };
        }
    }
}