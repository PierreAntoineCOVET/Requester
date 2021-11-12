using RequesterGui.Behaviors;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RequesterGui.ViewModels
{
	public class HostMenuItem : ISelectableItem
	{
		public string HostName { get; set; }

		public bool IsSelected { get; set; }

		public ObservableCollection<EndpointMenuItem> Endpoints { get; set; } = new ObservableCollection<EndpointMenuItem>();
	}

	public class EndpointMenuItem : ISelectableItem
	{
		public string EndpointName { get; set; }

		public bool IsSelected { get; set; }

		public HostMenuItem Host { get; set; }
	}
}
