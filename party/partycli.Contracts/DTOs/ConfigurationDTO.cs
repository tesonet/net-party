using partycli.Contracts.Exceptions;

namespace partycli.Contracts.DTOs
{
    public class ConfigurationDTO
    {
        public ConfigurationDTO(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new PartyException("Configuration username is required.");

            if (string.IsNullOrWhiteSpace(password))
                throw new PartyException("Configuration password is required.");

            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
