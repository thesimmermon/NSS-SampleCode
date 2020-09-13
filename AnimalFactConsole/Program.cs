// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the program class</summary>
namespace SampleCode
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using Autofac;
    using Serilog;

    /// <summary>A program.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    internal sealed class Program
    {
        /// <summary>Gets or sets the container.</summary>
        ///
        /// <value>The container.</value>
        private static IContainer Container { get; set; }

        /// <summary>Main entry-point for this application.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        private static void Main()
        {
            WriteHeaderToConsole();

            try
            {
                InitializeLogging();
                BuildContainer();

                using var scope = Container.BeginLifetimeScope();

                var pollingConfigs = JsonSerializer.Deserialize<List<PollingConfiguration>>(System.AppContext.GetData("pollingConfigs").ToString());
                var pollers = new List<IPollingEngine>();

                foreach (var config in pollingConfigs)
                {
                    var poller = scope.Resolve<IPollingEngine>();
                    pollers.Add(poller);
                    poller.Start(config);
                }

                Console.ReadLine();

                pollers.ForEach(p => p.Stop());
            }
            catch (Exception e)
            {
                Log.Error(e, "A general error occurred while trying to run the application.");
                WriteErrorToConsole(e);
            }
        }

        /// <summary>Writes an error to console.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
        ///
        /// <param name="e">An Exception to process.</param>
        private static void WriteErrorToConsole(Exception e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"An error occurred while trying to create Serilog instance.  {e.Message}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press Return to exit application");
            Console.ReadLine();
        }

        /// <summary>Writes the header to console.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
        private static void WriteHeaderToConsole()
        {
            Console.Title = "Animal Fact Console Application - Press [Return] Key to Exit";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*********************************************************************");
            Console.WriteLine("**");
            Console.WriteLine("** Animal Fact Console Application");
            Console.WriteLine("**   Press [Return] Key to Exit");
            Console.WriteLine("**");
            Console.WriteLine("*********************************************************************");
            Console.ResetColor();
        }

        /// <summary>Initializes the logging.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
        private static void InitializeLogging()
        {
            if (!(System.AppContext.GetData("seriLogFile") is string logFile))
            {
                logFile = Path.Combine(
                        AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                        "AnimalFactConsole.serilog.log");
            }

            var log = new LoggerConfiguration()
                .WriteTo.File(logFile)
                .CreateLogger();

            Log.Logger = log;
        }

        /// <summary>Builds the container.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        private static void BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleFactLogger>().As<IAnimalFactLogger>();
            builder.RegisterType<AnimalFacts>().As<IAnimalFacts>();
            builder.RegisterType<PollingEngine>().As<IPollingEngine>().InstancePerDependency();
            Container = builder.Build();
        }
    }
}
