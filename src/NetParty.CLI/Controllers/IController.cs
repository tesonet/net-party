using System.Threading.Tasks;

namespace NetParty.CLI.Controllers
{
    public interface IController<TOptions>
    {
        Task Handle(TOptions options);
    }
}