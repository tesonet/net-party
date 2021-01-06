namespace Tesonet.ServerListApp.Domain
{
    using System;

    public record Server(Guid Id, string Name, int Distance)
    {
    }
}