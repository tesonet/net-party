using NetParty.Application.Interfaces;
using NetParty.Application.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetParty.Application.Extensions
{
    public static class EntityMapping
    {
        public static IList<Domain.Entities.Server> MapToEntities(this IList<IServer> remoteServers)
        {
            return remoteServers.Select(server => new Domain.Entities.Server()
            {
                Name = server.Name,
                Distance = server.Distance
            })
            .ToList();
        }

        public static List<ServerDto> MapToDto(this IList<Domain.Entities.Server> serverEntities)
        {
            return serverEntities.Select(server => new ServerDto()
            {
                Name = server.Name,
                Distance = server.Distance
            })
            .ToList();
        }

        public static Domain.Entities.Credentials MapToEntity(this ICredentials credentials)
        {
            return new Domain.Entities.Credentials() {
                Username = credentials.Username,
                Password = credentials.Password
            };
        }

        public static Credentials.Models.Credentials MapToDto(this Domain.Entities.Credentials credentials)
        {
            return new Credentials.Models.Credentials()
            {
                Username = credentials.Username,
                Password = credentials.Password
            };
        }
    }
}
