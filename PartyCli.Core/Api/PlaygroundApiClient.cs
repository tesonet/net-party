using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Models;
using PartyCli.Core.Api.Response;

namespace PartyCli.Core.Api
{
	internal class PlaygroundApiClient : IPlaygroundApiClient
	{
		private readonly HttpClient _httpClient;

		public PlaygroundApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> GetToken(string userName, string password)
		{
			var requestBody = new
			{
				username = userName,
				password
			};

			var response = await _httpClient.PostAsJsonAsync("tokens", requestBody);

			if (!response.IsSuccessStatusCode)
			{
				throw new PartyCliException($"Could not retrieve auth token :(. Response code: {response.StatusCode}");
			}

			var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
			return token?.Token;
		}

		public async Task<IEnumerable<Server>> GetServers(string token)
		{
			var httpRequest = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("servers", UriKind.Relative)
			};
			httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(httpRequest);

			if (!response.IsSuccessStatusCode)
			{
				throw new PartyCliException($"Could not retrieve server list :(. Response code: {response.StatusCode}");
			}

			return await response.Content.ReadFromJsonAsync<IEnumerable<Server>>();
		}
	}
}