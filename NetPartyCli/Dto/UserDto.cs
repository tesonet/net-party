using System;

namespace NetPartyCli.Dto
{
    public class UserDto
    {
        public UserDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
