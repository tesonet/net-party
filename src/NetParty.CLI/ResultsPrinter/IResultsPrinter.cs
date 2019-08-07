using NetParty.Contracts.Results;

namespace NetParty.CLI.ResultsPrinter
{
    public interface IResultsPrinter
    {
        void Print(ServerList servers);
        void Print(AuthorizationResult authorizationResult);
        void Print(Error error);
    }
}