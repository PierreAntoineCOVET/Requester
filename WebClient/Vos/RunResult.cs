using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Vos
{
	public class RunResult
	{
		public IEnumerable<HttpResponse> httpResponses { get; set; }

		public double AverageTimeMs { get; set; }

		public long LongestTimeMs { get; set; }

		public long ShortestTimeMs { get; set; }

		public string Uri { get; set; }

		public int Times { get; set; }
	}
}
