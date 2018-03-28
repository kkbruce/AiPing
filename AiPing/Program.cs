﻿using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Diagnostics;

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
            TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
            TelemetryConfiguration.Active.InstrumentationKey = aikey;
            var client = new TelemetryClient();

            for (int i = 0; i < num; i++)
            {
                var id = i + 1;
                Console.WriteLine($"AiPing {id} Start:");

                Trace.TraceInformation($"TraceInformation{id}");
                Trace.TraceWarning($"TraceWarning{id}");
                Trace.TraceError($"TraceError{id}");
                Console.WriteLine("\t Trace ping end.");

                client.TrackTrace($"HelloWorld{id}!");
                client.TrackEvent($"HelloWorld!{id}");
                Console.WriteLine("\t AI Ping end");
            }
            Console.WriteLine("AiPing End.");

            Console.ReadLine();
        }
    }
}