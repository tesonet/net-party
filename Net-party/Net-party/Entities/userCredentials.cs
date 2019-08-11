using System.Data.Linq.Mapping;
using Net_party.CommandLineModels;

namespace Net_party.Entities
{
    [Table(Name = "UsersConfiguration")]
    public class UserCredentials
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int? Id { get; set; }

        [Column(Name = "Username")]
        public string Username { get; set; }

        [Column(Name = "Password")]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Username)}: {Username}, {nameof(Password)}: {Password}";
        }

        public static UserCredentials FromCredentialsDto(CredentialsDto credentials)
        {
            return new UserCredentials {Username = credentials.Username, Password = credentials.Password};
        }
    }
}
