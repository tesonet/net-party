﻿using Microsoft.Extensions.Caching.Memory;
using net_party.Services.Contracts;
using System;

namespace net_party.Services
{
    public class ServerService : BaseRestService, IServerService
    {
        private readonly IMemoryCache _memoryCache;

        public ServerService(IServiceProvider serviceProvider, IMemoryCache memoryCache) : base(serviceProvider)
        {
            _memoryCache = memoryCache;
        }
    }
}