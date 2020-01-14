using Microsoft.Extensions.DependencyInjection;
using net_party.Entities.API;
using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using net_party.Services.Base;
using net_party.Services.Contracts;
using RestSharp;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace net_party.Services
{
    public class AuthService : BaseRestService, IAuthService
    {
        private const string TOKEN = "/tokens";

        private readonly IAuthTokenRepository _authTokenRepository;
        private readonly ICredentialRepository _credentialRepository;
        private readonly IPasswordService _passwordService;

        public AuthService(IServiceProvider services) : base(services)
        {
            _authTokenRepository = services.GetService<IAuthTokenRepository>();
            _credentialRepository = services.GetService<ICredentialRepository>();
            _passwordService = services.GetService<IPasswordService>();
        }

        public async Task AuthenticateCredentials(string username, string password)
        {
            Console.WriteLine("Attempting to authenticate...");

            var request = BaseRequest(TOKEN, Method.POST);
            request.AddJsonBody(TokenRequest.New(username, password));

            var response = await ExecuteAsync<TokenResponse>(request);

            if (response == null)
            {
                Console.WriteLine("Failed to authenticate with the provided credentials.");
                return;
            }

            await HandleSuccessfulResponse(username, password, response);
        }

        private async Task HandleSuccessfulResponse(string username, string password, TokenResponse response)
        {
            var token = AuthToken.FromTokenResponse(response);

            var existingToken = await _authTokenRepository.Get();

            var connection = _services.GetService<SqlConnection>();
            await connection.OpenAsync();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    if (existingToken == null)
                        await _authTokenRepository.Add(token);
                    else
                    {
                        token.Id = existingToken.Id;
                        await _authTokenRepository.Update(token);
                    }

                    await StoreCredentials(username, password);

                    transaction.Commit();

                    Console.WriteLine("Authentication credentials updated successfully!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Authentication failed: {ex.Message}");
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        private async Task StoreCredentials(string username, string password)
        {
            var hashedPassword = _passwordService.HashPassword(password);
            var existing = await _credentialRepository.Get();

            if (existing == null)
                await _credentialRepository.Add(Credential.New(username, hashedPassword));
            else
            {
                existing.Username = username;
                existing.Password = hashedPassword;
                await _credentialRepository.Update(existing);
            }
        }
    }
}