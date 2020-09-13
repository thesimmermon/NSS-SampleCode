// <copyright file="Status.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the status class</summary>
namespace SampleCode
{
    using System.Text.Json.Serialization;

    /// <summary>A status.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    public class Status
    {
        /// <summary>Gets or sets a value indicating whether the verified.</summary>
        ///
        /// <value>True if verified, false if not.</value>
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        /// <summary>Gets or sets the number of sents.</summary>
        ///
        /// <value>The number of sents.</value>
        [JsonPropertyName("sentCount")]
        public int SentCount { get; set; }
    }
}
