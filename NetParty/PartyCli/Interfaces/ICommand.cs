using McMaster.Extensions.CommandLineUtils;

namespace PartyCli.Interfaces
{
    public interface ICommand
    {
        void Configure(CommandLineApplication command);
    }
}
