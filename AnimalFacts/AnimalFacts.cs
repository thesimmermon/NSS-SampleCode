// <copyright file="AnimalFacts.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the animal facts class</summary>
namespace SampleCode
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>An animal facts.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    ///
    /// <seealso cref="IAnimalFacts"/>
    public class AnimalFacts : IAnimalFacts
    {
        /// <summary>Options for controlling the JSON.</summary>
        private readonly JsonSerializerOptions jsonOptions;

        /// <summary>The HTTP client.</summary>
        private readonly HttpClient httpClient;

        /// <summary>True to disposed value.</summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalFacts"/> class.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        public AnimalFacts()
            : this(new HttpClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalFacts"/> class.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="httpClient">The HTTP client.</param>
        public AnimalFacts(HttpClient httpClient)
        {
            this.jsonOptions = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            this.httpClient = httpClient;
        }

        /// <summary>Gets random facts.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="animal">(Optional) The animal.</param>
        /// <param name="amount">(Optional) Number of facts to return.</param>
        ///
        /// <returns>The random facts.</returns>
        ///
        /// <seealso cref="IAnimalFacts.GetRandomFactsAsync(string,int?)"/>
        public async Task<IEnumerable<AnimalFact>> GetRandomFactsAsync(string animal = null, int? amount = null)
        {
            var response = await this.httpClient.GetAsync($"https://cat-fact.herokuapp.com/facts/random?animal_type={animal}&amount={amount}");
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<AnimalFact>>(text, this.jsonOptions);
            }
            else
            {
                return null;
            }
        }

        /// <summary>Gets random fact.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="animal">(Optional) The animal.</param>
        ///
        /// <returns>The random fact.</returns>
        ///
        /// <seealso cref="IAnimalFacts.GetRandomFactAsync(string)"/>
        public async Task<AnimalFact> GetRandomFactAsync(string animal = null)
        {
            var response = await this.httpClient.GetAsync($"https://cat-fact.herokuapp.com/facts/random?animal_type={animal}");
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AnimalFact>(text, this.jsonOptions);
            }
            else
            {
                return null;
            }
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

        /// <summary>Releases the unmanaged resources used by the NssLabs.AnimalFacts and optionally
        /// releases the managed resources.</summary>
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
                    this.httpClient.Dispose();
                }

                this.disposedValue = true;
            }
        }
    }
}
