using System.Collections.Generic;

namespace WebClient.Vos
{
	public class HostConfiguration
	{
		public bool IsUsingWindowsAuthentication { get; set; }

		public bool IsUsingJwtAuthentication { get; set; }

		public string HostBaseUri { get; set; }

		public string HostNameAlias { get; set; }

		public IEnumerable<EndpointConfiguration> Endpoints { get; set; }
	}
}
