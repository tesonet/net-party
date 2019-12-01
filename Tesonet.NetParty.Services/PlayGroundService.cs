using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Tesonet.NetParty.Models.Interfaces;
using Tesonet.NetParty.Models.Models;

namespace Tesonet.NetParty.Services
{
	public class PlayGroundService : IDataProvider
	{
		public string GetAuthenticationToken(User user)
		{
			using (var client = new HttpClient())
			{
				var authenticationJson = JsonConvert.SerializeObject(user).ToLower();
				using (var httpContent = new StringContent(authenticationJson, Encoding.UTF8, "application/json"))
				{
					var postResult = client.PostAsync(ConfigurationManager.AppSettings["GetAccessTokenUrl"], httpContent).Result;
					if (!postResult.IsSuccessStatusCode && postResult.StatusCode == HttpStatusCode.Unauthorized)
						throw new UnauthorizedAccessException();
					postResult.EnsureSuccessStatusCode();
					var result = postResult.Content.ReadAsStringAsync().Result;
					var accessToken = JsonConvert.DeserializeObject<AccessToken>(result);
					return accessToken.Token;
				}
			}
		}

		public IList<Server> GetServiceList(string accessToken)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				var postResult = client.GetAsync(ConfigurationManager.AppSettings["GetServiceList"]).Result;
				postResult.EnsureSuccessStatusCode();
				var result = postResult.Content.ReadAsStringAsync().Result;
				var list = JsonConvert.DeserializeObject<List<Server>>(result);
				return list;
			}
		}
	}
}
