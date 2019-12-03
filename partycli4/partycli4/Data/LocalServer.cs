
using CommandLine;

namespace partycli4.Data
{
    public class LocalServer
    {
        [Option("local", Required = false)]
        public bool IsLocal { get; set; }
    }
}
