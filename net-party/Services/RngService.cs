using Microsoft.Extensions.DependencyInjection;
using net_party.Services.Base;
using net_party.Services.Contracts;
using System;
using System.Security.Cryptography;

namespace net_party.Services
{
    public class RngService : BaseService, IRngService
    {
        private readonly RNGCryptoServiceProvider _rng;

        public RngService(IServiceProvider services) : base(services) => _rng = services.GetService<RNGCryptoServiceProvider>();

        public byte[] Bytes(int size)
        {
            var bytes = new byte[size];

            _rng.GetBytes(bytes);

            return bytes;
        }
    }
}
