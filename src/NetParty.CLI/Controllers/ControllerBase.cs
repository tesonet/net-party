using NetParty.CLI.ResultsPrinter;

namespace NetParty.CLI.Controllers
{
    public class ControllerBase
    {
        protected IResultsPrinter ResultsPrinter;

        public ControllerBase(IResultsPrinter resultsPrinter)
        {
            ResultsPrinter = resultsPrinter;
        }
    }
}