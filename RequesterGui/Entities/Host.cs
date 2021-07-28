using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Entities
{
	class Host
	{
		public Guid Id { get; set; }

		public bool IsUsingWindowsAuthentication { get; set; }

		public bool IsUsingJwtAuthentication { get; set; }

		public string HostBaseUri { get; set; }

		public string HostNameAlias { get; set; }

		public IEnumerable<Endpoint> Endpoints { get; set; }
	}
}
