using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Entities
{
	class HostEntity
	{
		public Guid Id { get; set; }

		public bool IsUsingWindowsAuthentication { get; set; }

		public bool IsUsingJwtAuthentication { get; set; }

		public string HostBaseUri { get; set; }

		public string HostNameAlias { get; set; }

		public IEnumerable<EndpointEntity> Endpoints { get; set; }
	}
}
