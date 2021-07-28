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
		public IList<Host> Hosts { get; set; }

		public void AddNewHost()
		{
			var newHost = new Host
			{

			};

			Hosts.Add(newHost);
		}
	}
}
