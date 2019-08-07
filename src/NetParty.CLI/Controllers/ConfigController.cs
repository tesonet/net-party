using System.Threading.Tasks;
using NetParty.CLI.Options;
using NetParty.CLI.ResultsPrinter;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Handlers;

namespace NetParty.CLI.Controllers
{
    public class ConfigController : ControllerBase, IController<ConfigOptions>
    {
        private readonly IRequestHandler<Authorize, AuthorizationResult> _whenAuthorize;

        public ConfigController(IRequestHandler<Authorize, AuthorizationResult> whenAuthorize,
            IResultsPrinter resultsPrinter) : base(resultsPrinter)
        {
            _whenAuthorize = whenAuthorize;
        }

        public async Task Handle(ConfigOptions options)
        {
            var request = MapOptionsToRequest(options);
            var result = await _whenAuthorize.ThenAsync(request).ConfigureAwait(false);
            ResultsPrinter.Print(result);
        }

        private Authorize MapOptionsToRequest(ConfigOptions options)
        {
            return new Authorize
            {
                Username = options.Username,
                Password = options.Password
            };
        }
    }
}
