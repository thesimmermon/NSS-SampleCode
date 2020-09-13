// <copyright file="PollingEngine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the polling engine class</summary>
namespace SampleCode
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Serilog;

    /// <summary>A polling engine.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    ///
    /// <seealso cref="IPollingEngine"/>
    public class PollingEngine : IPollingEngine
    {
        /// <summary>Source for the cancellation.</summary>
        private readonly CancellationTokenSource source = new CancellationTokenSource();

        /// <summary>The fact logger.</summary>
        private readonly IAnimalFactLogger factLogger;

        /// <summary>The animal facts.</summary>
        private readonly IAnimalFacts animalFacts;

        /// <summary>True to disposed value.</summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PollingEngine"/> class.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="factLogger"> The fact logger.</param>
        /// <param name="animalFacts">The animal facts.</param>
        public PollingEngine(IAnimalFactLogger factLogger, IAnimalFacts animalFacts)
        {
            this.factLogger = factLogger;
            this.animalFacts = animalFacts;
        }

        /// <summary>Starts.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="config">The configuration.</param>
        public void Start(PollingConfiguration config)
        {
            Log.Information("Starting polling engine for {animal} every {interval} seconds and returning {amount} fact(s)", config.Animal, config.Interval ?? 10, config.Amount ?? 1);

            Task.Factory.StartNew(async () =>
            {
                var token = this.source.Token;
                var interval = TimeSpan.FromSeconds(config.Interval ?? 10);

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var pollTimestamp = DateTime.Now;

                        if ((config.Amount ?? 1) == 1)
                        {
                            var fact = await this.animalFacts.GetRandomFactAsync(config.Animal);
                            await this.factLogger.WriteAsync(pollTimestamp, fact.Type, fact.Text);
                        }
                        else
                        {
                            var facts = await this.animalFacts.GetRandomFactsAsync(config.Animal, config.Amount);
                            foreach (var fact in facts)
                            {
                                await this.factLogger.WriteAsync(pollTimestamp, fact.Type, fact.Text);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Log.Warning(exception, $"An error occurred while trying to get the fact.");
                    }
                    finally
                    {
                        await Task.Delay(interval, token);
                    }
                }
            });
        }

        /// <summary>Stops this object.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        public void Stop()
        {
            this.source.Cancel();
            Log.Information($"Stopped polling engine");
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <seealso cref="IDisposable.Dispose()"/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="disposing">    True to release both managed and unmanaged resources; false to
        ///                             release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.source?.Dispose();
                }

                this.disposedValue = true;
            }
        }
    }
}
