using Microsoft.Extensions.Configuration;
using PartyCli.Infrastructure.Exeptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartyCli.Infrastructure
{
    public class Logger : Core.Interfaces.ILogger
    {
        private readonly Serilog.Core.Logger _logger;
     

        public Logger(IConfiguration configuration)
        {
            var LogFileName = configuration["LogFileName"] ??  "log-{Date}.log"; 
            _logger = new  Serilog.LoggerConfiguration()
                               .MinimumLevel.Debug()                               
                               .WriteTo.RollingFile(Path.Combine(Environment.CurrentDirectory, LogFileName))
                               .CreateLogger();

            if (configuration["LogFileName"] == null)
                _logger.Error("\"PartyCli.AppSettings.json\" section LogFileName not found. Using default name \"log-{Date}.log\"");
        }

        public void Error(string entry)
        {
            _logger.Error(entry);
        }

        public void Debug(string entry)
        {
            _logger.Debug(entry);
        }
    }
}
