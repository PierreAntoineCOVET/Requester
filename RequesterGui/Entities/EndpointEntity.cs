using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Entities
{
	class EndpointEntity
    {
        public Guid Id { get; set; }

        public string Method { get; set; }

        public string Path { get; set; }

        public string UriAlias { get; set; }

        public string Payload { get; set; }

        public Guid HostId { get; set; }

        public HostEntity Host { get; set; }
    }
}
