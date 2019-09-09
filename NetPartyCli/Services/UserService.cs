using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetPartyCli.Database;
using NetPartyCli.Dto;
using NetPartyCli.Repositories;

namespace NetPartyCli.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public Task AddAsync(UserDto userDto)
        {
            _logger.LogInformation("Adding user.");
            return _userRepository.SaveAsync(new User
            {
                Password = userDto.Password,
                Username = userDto.Username
            });
        }

        public async Task<UserDto> GetAsync()
        {
            _logger.LogInformation("Getting user.");
            var user = await _userRepository.GetAsync();
            return new UserDto(user.Username, user.Password);
        }
    }
}
