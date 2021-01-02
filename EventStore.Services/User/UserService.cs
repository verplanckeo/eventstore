using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.Application.Repositories.User;
using EventStore.Services.User.Models;

namespace EventStore.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IReadUserRepository _readUserRepository;

        public UserService(IUserRepository userRepository, IReadUserRepository readUserRepository)
        {
            _userRepository = userRepository;
            _readUserRepository = readUserRepository;
        }

        public async Task<string> RegisterUser(string userName, string firstName, string lastName)
        {
            var domainUser = Core.Domains.User.User.CreateNewUser(userName, firstName, lastName);
            var id = await _userRepository.SaveUserAsync(domainUser);
            return id.ToString();
        }

        public async Task<UserModel> LoadUser(string userId)
        {
            var domainUser = await _userRepository.LoadUserAsync(userId);
            return UserModel.FromDomain(domainUser);
        }

        public async Task<IEnumerable<ReadUserModel>> LoadUsersOverview()
        {
            var readUserModels = await _readUserRepository.LoadUsersAsync();
            return readUserModels.Select(ReadUserModel.FromEntity);
        }
    }
}