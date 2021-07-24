using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterCli
{
    class CommandLineOptions
    {
        [Option('h', "hostAlias", Required = true, HelpText = "Host alias as defined in appsettings.json")]
        public string HostAlias { get; set; }

        [Option('e', "endpoint", Required = true, HelpText = "Host endpoind as defined in appsettings.json")]
        public string Endpoint { get; set; }

        [Option('t', "times", Required = false, HelpText = "Number of query execution", Default = 1)]
        public int Times { get; set; }

        [Option('r', "displayRequestsContent", Required = false, HelpText = "If true then display the queries responses as string", Default = false)]
        public bool DisplayResponseContent { get; set; }

        [Option('t', "displayMinMaxTimes", Required = false, HelpText = "If true then display min and max response times", Default = false)]
        public bool DisplayMinMaxTimes { get; set; }
    }
}
