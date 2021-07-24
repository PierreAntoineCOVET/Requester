using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClient.Config;

namespace WebClient.Vos
{
	public class HostConfiguration
	{
		public bool IsUsingWindowsAuthentication { get; set; }

		public bool IsUsingJwtAuthentication { get; set; }

		public string HostBaseUri { get; set; }

		public string HostNameAlias { get; set; }

		public IEnumerable<UriConfiguration> Endpoints { get; set; }
	}
}
