using Microsoft.EntityFrameworkCore;
using RequesterGui.Behaviors;
using RequesterGui.Core;
using RequesterGui.Core.Commands;
using RequesterGui.DbContext;
using RequesterGui.Mapping;
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

		private readonly RequesterDbContext DbContext;

		public MainViewModel()
		{
			CreateNewHost = new RelayCommand(ExecuteCreateNewHostCommand);
			CreateNewEndpoint = new RelayCommand(ExecuteCreateNewEndpoint);
			SaveConfig = new AsyncCommand(ExecuteSaveConfigCommand);

			DbContext = new RequesterDbContext();

			_ = LoadDatas();
		}

		private void SortHosts(IEnumerable<HostViewModel> hosts = null)
		{
			// defered execution is ruined by the clear
			var orderedHosts = hosts == null
				? HostViewModels.OrderBy(h => h.HostNameAlias).ToList()
				: hosts.OrderBy(h => h.HostNameAlias).ToList();

			HostViewModels?.Clear();

			foreach (var host in orderedHosts)
			{
				HostViewModels.Add(host);
			}
		}

		private async Task LoadDatas()
		{
			var hosts = await DbContext.Hosts
				.Include(h => h.Endpoints)
				.AsNoTracking()
				.ToListAsync();

			SortHosts(hosts.Select(h => HostMapper.EntityToViewModel(h)));
		}

		private void ExecuteCreateNewEndpoint()
		{
			if(_SelectedItem == null)
			{
				return;
			}

			_SelectedItem.Endpoints.Add(new EndpointViewModel { UriAlias = $"Endpoint n°{SelectedItem.Endpoints.Count + 1}" });
		}

		private void ExecuteCreateNewHostCommand()
		{
			HostViewModels.Add(new HostViewModel { HostNameAlias = $"Host n°{HostViewModels.Count + 1}", IsSelected = true });

			SortHosts();
		}

		private async Task ExecuteSaveConfigCommand()
		{
			var hostEntities = HostViewModels
				.Select(h => HostMapper.ViewModelToEntity(h))
				.ToList();

			DbContext.UpdateRange(hostEntities);

			await DbContext.SaveChangesAsync();
		}
	}
}
