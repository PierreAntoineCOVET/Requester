using RequesterGui.Behaviors;
using RequesterGui.Core;
using RequesterGui.Core.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WebClient.Vos;

namespace RequesterGui.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		private List<HostConfiguration> HostViewModels { get; set; } = new List<HostConfiguration>();

		public ObservableCollection<HostMenuItem> HostMenuItems { get; set; } = new ObservableCollection<HostMenuItem>();

		private ISelectableItem _SelectedItem { get; set; }
		public ISelectableItem SelectedItem 
		{
			get => _SelectedItem;
			set
			{
				_SelectedItem = value;
				OnSelectedItemChanged();
			}
		}

		public ICommand CreateNewHost { get; set; }

		public ICommand CreateNewEndpoint { get; set; }

		public IAsyncCommand SaveConfig { get; set; }

		public MainViewModel()
		{
			CreateNewHost = new RelayCommand(ExecuteCreateNewHostCommand);
			CreateNewEndpoint = new RelayCommand(ExecuteCreateNewEndpoint);
			SaveConfig = new AsyncCommand(ExecuteSaveConfigCommand);
		}

		private void OnSelectedItemChanged()
		{
			if(_SelectedItem is HostMenuItem hostMenuItem)
			{
				
			}
			else if(_SelectedItem is EndpointMenuItem endpointMenuItem)
			{

			}
		}

		private UriConfiguration GetEndPointConfiguration(EndpointMenuItem endpointMenuItem, HostConfiguration hostConfiguration)
		{
			return hostConfiguration.Endpoints.SingleOrDefault(e => e.UriAlias == endpointMenuItem.EndpointName);
		}

		private HostConfiguration GetHostConfiguration(HostMenuItem hostMenuItem)
		{
			return HostViewModels.SingleOrDefault(h => h.HostNameAlias == hostMenuItem.HostName);
		}

		private void ExecuteCreateNewEndpoint()
		{
			var selectedHost = HostMenuItems.SingleOrDefault(h => h.IsSelected);

			if(selectedHost != null)
			{
				selectedHost.Endpoints.Add(new EndpointMenuItem
				{
					EndpointName = "MyEndpoint",
					Host = selectedHost
				});
			}
		}

		private void ExecuteCreateNewHostCommand()
		{
			var hostMenuItem = new HostMenuItem
			{
				HostName = $"Host n°{HostMenuItems.Count}",
			};

			HostMenuItems.Add(hostMenuItem);
		}

		private async Task ExecuteSaveConfigCommand()
		{
			await Task.Delay(10);
		}
	}
}
