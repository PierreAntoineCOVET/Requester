using RequesterGui.Core;
using RequesterGui.Core.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WebClient.Vos;

namespace RequesterGui.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public ObservableCollection<HostConfiguration> HostViewModels { get; set; } = new ObservableCollection<HostConfiguration>();

		public ICommand CreateNewHost { get; set; }

		public IAsyncCommand SaveConfig { get; set; }

		public MainViewModel()
		{
			CreateNewHost = new RelayCommand(ExecuteCreateNewHostCommand);
			SaveConfig = new AsyncCommand(ExecuteSaveConfigCommand);
		}

		private void ExecuteCreateNewHostCommand()
		{
			HostViewModels.Add(new HostConfiguration
			{
				HostBaseUri = "TestUri",
				IsUsingJwtAuthentication = true
			});
		}

		private async Task ExecuteSaveConfigCommand()
		{
			await Task.Delay(10);
		}
	}
}
