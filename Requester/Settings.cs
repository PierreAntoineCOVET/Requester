using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClient.Vos;

namespace Requester
{
	public class Settings
	{
		public IEnumerable<HostConfiguration> HostSettings { get; set; }
	}
}
