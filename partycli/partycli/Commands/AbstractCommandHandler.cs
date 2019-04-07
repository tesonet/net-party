using partycli.core.Execution;
using partycli.core.Logging;
using partycli.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Commands
{
    abstract class AbstractCommandHandler
    {
        protected IExecutor executor;
        protected ILogger logger;
        protected IConsoleWriter writer;

        public AbstractCommandHandler(IExecutor executor, ILogger logger, IConsoleWriter writer)
        {
            this.executor = executor;
            this.logger = logger;
            this.writer = writer;
        }
    }
}
