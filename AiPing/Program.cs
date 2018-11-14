using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AiPing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("AiPing.exe number aikey");
                Console.ReadLine();
                return;
            }

            int.TryParse(args[0], out int num);
            string aikey = args[1];

            // set develop mode
            //TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
            TelemetryConfiguration.Active.InstrumentationKey = aikey;
            var client = new TelemetryClient();
            client.Context.User.Id = "AiPing";
            client.Context.Device.Id = System.Net.Dns.GetHostName();

            try
            {
                for (int i = 0; i < num; i++)
                {
                    var id = i + 1;
                    Console.WriteLine($"AiPing {id} Start:");

                    Trace.TraceInformation($"TraceInformation {id}.");
                    Trace.TraceWarning($"TraceWarning {id}.");
                    Trace.TraceError($"TraceError {id}.");
                    Console.WriteLine("\t Trace ping end.");

                    client.TrackTrace($"HelloWorld {id}!");
                    client.TrackEvent($"HelloWorld {id}!");
                    var properties = new Dictionary<string, string>
                                        {{"AiException", "AiException Text"}};
                    var measurements = new Dictionary<string, double>
                                        {{"AiMeasure",100.0 }};
                    client.TrackException(new Exception($"AiException {id}"), properties, measurements);
                    Console.WriteLine("\t AI Ping end");
                    Thread.Sleep(100);
                }
                Console.WriteLine("AiPing End.");
            }
            catch (Exception ex)
            {
                client.TrackException(ex);
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}