using System.Threading.Tasks;

namespace partycli.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task Handle(T command);
    }
}
