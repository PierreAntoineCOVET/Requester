using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.ViewModels
{
	public class EndpointViewModel
	{
		public string Method { get; set; }

		public string Path { get; set; }

		public string UriAlias { get; set; }

		public string Payload { get; set; }
	}
}
