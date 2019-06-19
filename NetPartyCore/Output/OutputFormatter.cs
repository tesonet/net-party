using System;

namespace NetPartyCore.Output
{
    class OutputFormatter : IOutputFormatter
    {
        public void TestMethod(string output)
        {
            Console.WriteLine($"OUTPUT: {output}");
        }
    }
}
