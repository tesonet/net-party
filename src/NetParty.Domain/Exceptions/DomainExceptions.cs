namespace NetParty.Domain.Exceptions
{
    public class DomainExceptions
    {
        public static readonly DomainException NoLocalServersFound = new DomainException(nameof(NoLocalServersFound), "No locally persisted servers to retrieve.");
        public static readonly DomainException NotAuthorized = new DomainException(nameof(NotAuthorized), "Please authenticate using the `config` verb.");
        public static readonly DomainException CouldNotRetrieveServers = new DomainException(nameof(CouldNotRetrieveServers), "Could not retrieve servers list, please try again later.");
        public static readonly DomainException CouldNotRetrieveLocalServers = new DomainException(nameof(CouldNotRetrieveLocalServers), "Could not retrieve local servers list, please try calling `server_list` without the local flag.");
    }
}