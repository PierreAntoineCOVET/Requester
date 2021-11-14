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
		//private List<HostConfiguration> Hosts { get; set; } = new List<HostConfiguration>();

		public ObservableCollection<HostViewModel> HostViewModels { get; set; } = new ObservableCollection<HostViewModel>();

		private HostViewModel _SelectedItem { get; set; }
		public HostViewModel SelectedItem 
		{
			get => _SelectedItem;
			set
			{
				if (_SelectedItem == value)
				{
					return;
				}

				_SelectedItem = value;
				OnPropertyChanged();
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

		private void ExecuteCreateNewEndpoint()
		{
			SelectedItem.Endpoints.Add(new EndpointViewModel { UriAlias = $"Endpoint n°{SelectedItem.Endpoints.Count + 1}" });
		}

		private void ExecuteCreateNewHostCommand()
		{
			HostViewModels.Add(new HostViewModel { HostNameAlias = $"Host n°{HostViewModels.Count + 1}" });
		}

		private async Task ExecuteSaveConfigCommand()
		{
			await Task.Delay(10);
		}
	}
}
