namespace PartyCLI.ConsoleOutputWriters
{
    using System.Text;

    public class StringBuilderOutputProvider : IOutputProvider
    {
        private readonly StringBuilder stringBuilder;

        public StringBuilderOutputProvider(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }

        public void WriteLine(string text)
        {
            stringBuilder.AppendLine(text);
        }
    }
}