// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace McMaster.Extensions.Hosting.CommandLine.Custom
{
    using CommandLineUtils;
    using CommandLineUtils.Abstractions;

    /// <summary>
    /// A DI container for storing command line arguments.
    /// </summary>
    public class CommandLineState : CommandLineContext
    {
        public CommandLineState(string[] args)
        {
            Arguments = args;
        }

        internal void SetConsole(IConsole console)
        {
            Console = console;
        }

        public int ExitCode { get; set; }
    }
}