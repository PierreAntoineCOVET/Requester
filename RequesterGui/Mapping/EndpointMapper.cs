using RequesterGui.Entities;
using RequesterGui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Mapping
{
	static class EndpointMapper
	{
		public static EndpointEntity ViewModelToEntity(EndpointViewModel endpointViewModel)
		{
			return new EndpointEntity
			{
				HostId   = endpointViewModel.HostId,
				Id       = endpointViewModel.Id,
				Method   = endpointViewModel.Method,
				Path     = endpointViewModel.Path,
				Payload  = endpointViewModel.Payload,
				UriAlias = endpointViewModel.UriAlias
			};
		}

		public static EndpointViewModel EntityToViewModel(EndpointEntity endpointEntity)
		{
			return new EndpointViewModel
			{
				HostId   = endpointEntity.HostId,
				Id       = endpointEntity.Id,
				Method   = endpointEntity.Method,
				Path     = endpointEntity.Path,
				Payload  = endpointEntity.Payload,
				UriAlias = endpointEntity.UriAlias
			};
		}
	}
}
