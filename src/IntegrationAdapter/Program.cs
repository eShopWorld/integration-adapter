﻿using System;
using System.Diagnostics;
using System.Threading;
using Eshopworld.Telemetry;
using Eshopworld.Web;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Runtime;

namespace IntegrationAdapter
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                if (EnvironmentHelper.IsInFabric)
                {
                    // The ServiceManifest.XML file defines one or more service type names.
                    // Registering a service maps a service type name to a .NET type.
                    // When Service Fabric creates an instance of this service type,
                    // an instance of the class is created in this host process.

                    ServiceRuntime.RegisterServiceAsync("IntegrationAdapterType",
                        context => new IntegrationAdapter(context)).GetAwaiter().GetResult();

                    ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(IntegrationAdapter).Name);

                    // Prevents this host process from terminating so services keeps running. 
                    Thread.Sleep(Timeout.Infinite);
                }
                else
                {
                    var host = WebHost.CreateDefaultBuilder()
                        .UseStartup<Startup>()
                        .Build();

                    host.Run();
                }
            }
            catch (Exception e)
            {
                BigBrother.Write(e);
                throw;
            }
        }
    }
}
