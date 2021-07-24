using Flurl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebClient.Config;
using WebClient.Vos;

namespace WebClient
{
	public class RequestManager
    {
        private readonly IEnumerable<HostConfiguration> HostConfiguration;

        private readonly HttpClient HttpClient;

        public RequestManager(IEnumerable<HostConfiguration> hostConfiguration)
        {
            HostConfiguration = hostConfiguration;

            HttpClient = new HttpClient();
        }

        public async Task<RunResult> Request(string hostAlias, string endpointAlias, int times)
        {
            var targetHost = HostConfiguration.SingleOrDefault(h => h.HostNameAlias == hostAlias);

            if(targetHost == null)
            {
                throw new ArgumentException($"Host '{hostAlias}' not found in config file.");
            }

            var endpoint = targetHost.Endpoints.SingleOrDefault(e => e.UriAlias == endpointAlias);

            if (endpoint == null)
            {
                throw new ArgumentException($"Host '{endpointAlias}' not found in config file.");
            }

            if(times < 0)
            {
                throw new ArgumentException($"Times must be a positive inter.");
            }

            var uri = BuildUri(targetHost.HostBaseUri, endpoint);
            var stopWatch = new Stopwatch();
            var httpResponses = new List<HttpResponse>();

            for (int i = 0; i < times; i++)
            {
                var request = new HttpRequestMessage(new HttpMethod(endpoint.Method), uri);

                stopWatch.Start();
                var response = await HttpClient.SendAsync(request);
                stopWatch.Stop();

                httpResponses.Add(await HandleResponse(response, stopWatch.ElapsedMilliseconds));

                stopWatch.Reset();
            }

            return ComputeRunResult(httpResponses, uri, times);
        }

        private RunResult ComputeRunResult(IEnumerable<HttpResponse> httpResponses, Uri uri, int times)
		{
            var requestTimes = httpResponses.Select(r => r.ResponseTimeMs).ToList();
            return new RunResult
            {
                AverageTimeMs = requestTimes.Average(),
                httpResponses = httpResponses,
                LongestTimeMs = requestTimes.Max(),
                ShortestTimeMs = requestTimes.Min(),
                Times = times,
                Uri = uri.AbsoluteUri
            };
		}

		private async Task<HttpResponse> HandleResponse(HttpResponseMessage response, long elapsedMs)
		{
            return new HttpResponse
            {
                Content        = await response.Content.ReadAsStringAsync(),
                ResponseTimeMs = elapsedMs,
                Status         = response.StatusCode.ToString()
            };

        }

		private Uri BuildUri(string hostBaseUri, UriConfiguration uriConfiguration)
        {
            var url = new Url(hostBaseUri)
                .AppendPathSegment(uriConfiguration.Path);

			if (uriConfiguration.Payload?.Any() == true)
            {
                url = url.SetQueryParams(uriConfiguration.Payload);

            }

			return url.ToUri();
        }
    }
}
