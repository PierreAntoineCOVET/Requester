using RequesterGui.Entities;
using RequesterGui.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Mapping
{
	static class HostMapper
	{
		public static HostEntity ViewModelToEntity(HostViewModel hostViewModel)
		{
			var hostEntity = new HostEntity
			{
				HostBaseUri                  = hostViewModel.HostBaseUri,
				HostNameAlias                = hostViewModel.HostNameAlias,
				Id                           = hostViewModel.Id,
				IsUsingJwtAuthentication     = hostViewModel.IsUsingJwtAuthentication,
				IsUsingWindowsAuthentication = hostViewModel.IsUsingWindowsAuthentication
			};

			if(hostViewModel.Endpoints?.Any() == true)
			{
				hostEntity.Endpoints = hostViewModel.Endpoints
					.Select(e => EndpointMapper.ViewModelToEntity(e))
					.ToList();
			}

			return hostEntity;
		}

		public static HostViewModel EntityToViewModel(HostEntity hostEntity)
		{
			var hostViewModel = new HostViewModel
			{
				HostBaseUri                  = hostEntity.HostBaseUri,
				HostNameAlias                = hostEntity.HostNameAlias,
				Id                           = hostEntity.Id,
				IsUsingJwtAuthentication     = hostEntity.IsUsingJwtAuthentication,
				IsUsingWindowsAuthentication = hostEntity.IsUsingWindowsAuthentication
			};

			if(hostEntity.Endpoints?.Any() == true)
			{
				hostViewModel.Endpoints = new ObservableCollection<EndpointViewModel>(
					hostEntity.Endpoints
					.Select(e => EndpointMapper.EntityToViewModel(e))
					.OrderBy(e => e.UriAlias));
			}

			return hostViewModel;
		}
	}
}
