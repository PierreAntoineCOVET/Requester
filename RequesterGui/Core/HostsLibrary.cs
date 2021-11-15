using RequesterGui.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Core
{
	class HostsLibrary
	{
		public IList<HostEntity> Hosts { get; set; }

		public void AddNewHost()
		{
			var newHost = new HostEntity
			{

			};

			Hosts.Add(newHost);
		}
	}
}
