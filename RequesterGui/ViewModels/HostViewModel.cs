using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.ViewModels
{
	public class HostViewModel
	{
		public bool IsUsingWindowsAuthentication { get; set; }

		public bool IsUsingJwtAuthentication { get; set; }

		public string HostBaseUri { get; set; }

		public string HostNameAlias { get; set; }

		public ObservableCollection<EndpointViewModel> Endpoints { get; set; } = new ObservableCollection<EndpointViewModel>();
	}
}
