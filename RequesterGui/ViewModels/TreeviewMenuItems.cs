using RequesterGui.Behaviors;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RequesterGui.ViewModels
{
	public class HostMenuItem
	{
		public string HostName { get; set; }

		public ObservableCollection<EndpointMenuItem> Endpoints { get; set; } = new ObservableCollection<EndpointMenuItem>();
	}

	public class EndpointMenuItem
	{
		public string EndpointName { get; set; }

		public HostMenuItem Host { get; set; }
	}
}
