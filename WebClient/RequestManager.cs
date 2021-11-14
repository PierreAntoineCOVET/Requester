using Flurl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebClient.Vos;

namespace WebClient
{
	public class RequestManager
	{
		private readonly IEnumerable<HostConfiguration> HostConfiguration;

		public RequestManager(IEnumerable<HostConfiguration> hostConfiguration)
		{
			HostConfiguration = hostConfiguration;
		}

		public async Task<RunResult> Request(string hostAlias, string endpointAlias, int times)
		{
			var targetHost = HostConfiguration.SingleOrDefault(h => h.HostNameAlias == hostAlias);

			if (targetHost == null)
			{
				throw new ArgumentException($"Host '{hostAlias}' not found in config file.");
			}

			var httpClient = GetHttpClient(targetHost);

			if (targetHost.IsUsingJwtAuthentication)
			{
				await SetToken(targetHost, httpClient);
			}

			var endpoint = targetHost.Endpoints.SingleOrDefault(e => e.UriAlias == endpointAlias);

			if (endpoint == null)
			{
				throw new ArgumentException($"Host '{endpointAlias}' not found in config file.");
			}

			if (times < 0)
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
				var response = await httpClient.SendAsync(request);
				stopWatch.Stop();

				httpResponses.Add(await HandleResponse(response, stopWatch.ElapsedMilliseconds));

				stopWatch.Reset();
			}

			return ComputeRunResult(httpResponses, uri, times);
		}

		private async Task SetToken(HostConfiguration hostConfiguration, HttpClient httpClient)
		{
			var jwtEndpoint = hostConfiguration.Endpoints.SingleOrDefault(e => e.UriAlias == "Jwt");

			if (jwtEndpoint == null)
			{
				throw new ArgumentException($@"If the host use JWT authentication then it must have a ""Jwt"" endpoint.");
			}

			var jwtUri = BuildUri(hostConfiguration.HostBaseUri, jwtEndpoint);
			var request = new HttpRequestMessage(new HttpMethod(jwtEndpoint.Method), jwtUri);

			var response = await httpClient.SendAsync(request);
			var jsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
			var accesToken = jsonDocument.RootElement.GetProperty("accessToken").GetString();

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesToken);
		}

		private HttpClient GetHttpClient(HostConfiguration hostConfiguration)
		{
			var httpClientHandler = new HttpClientHandler { AllowAutoRedirect = false };

			if (hostConfiguration.IsUsingWindowsAuthentication)
			{
				httpClientHandler.UseDefaultCredentials = true;
			}

			var httpClient = new HttpClient(httpClientHandler);

			return httpClient;
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
				Content = await response.Content.ReadAsStringAsync(),
				ResponseTimeMs = elapsedMs,
				Status = response.StatusCode.ToString()
			};

		}

		private Uri BuildUri(string hostBaseUri, EndpointConfiguration uriConfiguration)
		{
			var url = new Url(hostBaseUri)
				.AppendPathSegment(uriConfiguration.Path);

			if (!string.IsNullOrWhiteSpace(uriConfiguration.Payload))
			{
				var payloadObject = JsonSerializer.Deserialize<Dictionary<string, object>>(uriConfiguration.Payload);
				url = url.SetQueryParams(payloadObject);

			}

			return url.ToUri();
		}
	}
}
