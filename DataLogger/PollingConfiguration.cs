// <copyright file="PollingConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/13/2020</date>
// <summary>Implements the polling configuration class</summary>
namespace SampleCode
{
    using System.Text.Json.Serialization;

    /// <summary>A polling configuration.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
    public class PollingConfiguration
    {
        /// <summary>Gets or sets the animal.</summary>
        ///
        /// <value>The animal.</value>
        [JsonPropertyName("animal")]
        public string Animal { get; set; }

        /// <summary>Gets or sets the interval.</summary>
        ///
        /// <value>The interval.</value>
        [JsonPropertyName("interval")]
        public int? Interval { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        ///
        /// <value>The amount.</value>
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }
    }
}
