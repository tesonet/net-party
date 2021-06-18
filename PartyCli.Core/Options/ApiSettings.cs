using System;

namespace PartyCli.Core.Options
{
	public class ApiSettings
	{
		public string BaseUrl { get; set; }

		public TimeSpan Timeout { get; set; }
	}
}