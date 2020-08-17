using McMaster.Extensions.CommandLineUtils;

namespace NetParty.Application.Commands
{
    public interface ICommand
    {
        void Attach(CommandLineApplication app);
    }
}