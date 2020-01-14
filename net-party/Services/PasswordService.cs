﻿using Microsoft.Extensions.DependencyInjection;
using net_party.Services.Base;
using net_party.Services.Contracts;
using System;
using System.Security.Cryptography;

namespace net_party.Services
{
    public class PasswordService : BaseService, IPasswordService
    {
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash
        public const int PBKDF2_ITERATIONS = 1000;
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        public PasswordService(IServiceProvider services) : base(services)
        {
        }

        public string HashPassword(string password)
        {
            var cryptoProvider = _services.GetService<IRngService>();
            byte[] salt = cryptoProvider.Bytes(SALT_BYTE_SIZE);

            var hash = GetPbkdf2Bytes(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            return $"{PBKDF2_ITERATIONS}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);

            var iterations = int.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
             => new Rfc2898DeriveBytes(password, salt, iterations).GetBytes(outputBytes);
    }
}