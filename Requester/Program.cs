using CommandLine;
using Microsoft.Extensions.Configuration;
using RequesterCli;
using System;
using System.IO;
using System.Threading.Tasks;
using WebClient;
using WebClient.Vos;

namespace Requester
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(
                    async (CommandLineOptions options) =>
                    {
                        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

                        var hostConfiguration = configuration.Get<Settings>();

                        var requestManager = new RequestManager(hostConfiguration.HostSettings);
                        var runResult = await requestManager.Request(options.HostAlias, options.Endpoint, options.Times);

                        DisplayRunResult(runResult, options);
                        return 0;
                    },
                    errs => Task.FromResult(-1)
                );
        }

		private static void DisplayRunResult(RunResult runResult, CommandLineOptions options)
		{
            PrintResult(runResult, options);

            if (! string.IsNullOrWhiteSpace(options.OutputFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(options.OutputFile));
                using (var writer = File.AppendText(options.OutputFile))
                {
                    var defaultOutput = Console.Out;
                    Console.SetOut(writer);

                    PrintResult(runResult, options);

                    Console.SetOut(defaultOutput);
                }
            }
        }

        private static void PrintResult(RunResult runResult, CommandLineOptions options)
        {
            Console.WriteLine("=================== Summary ===================");
            Console.WriteLine($@"Requested ""{runResult.Uri}"" {runResult.Times} time(s) with average time of {runResult.AverageTimeMs}ms");

            if(options.DisplayMinMaxTimes || options.DisplayResponseContent)
            {
                Console.WriteLine();

                Console.WriteLine("=================== Details ===================");
            }

            if (options.DisplayMinMaxTimes)
            {
                Console.WriteLine($"Min response time {runResult.ShortestTimeMs}ms, max response time {runResult.LongestTimeMs}ms");
            }

            if (options.DisplayResponseContent)
            {
                Console.WriteLine();
                foreach (var response in runResult.httpResponses)
                {
                    Console.WriteLine(response.Content);
                }
            }

            Console.WriteLine();
        }
	}
}
