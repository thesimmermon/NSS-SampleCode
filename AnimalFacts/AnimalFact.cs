// <copyright file="AnimalFact.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the animal fact class</summary>
namespace SampleCode
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>Values that represent sources.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Source
    {
        /// <summary>An enum constant representing the user option.</summary>
        User,

        /// <summary>An enum constant representing the API option.</summary>
        Api,
    }

    /// <summary>An animal fact.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    public class AnimalFact
    {
        /// <summary>Gets or sets the identifier.</summary>
        ///
        /// <value>The identifier.</value>
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        /// <summary>Gets or sets the version.</summary>
        ///
        /// <value>The version.</value>
        [JsonPropertyName("__v")]
        public int Version { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        ///
        /// <value>The identifier of the user.</value>
        [JsonPropertyName("user")]
        public string UserId { get; set; }

        /// <summary>Gets or sets the text.</summary>
        ///
        /// <value>The text.</value>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>Gets or sets the Date/Time of the updated timestamp.</summary>
        ///
        /// <value>The updated timestamp.</value>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedTimestamp { get; set; }

        /// <summary>Gets or sets the send date.</summary>
        ///
        /// <value>The send date.</value>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedTimestamp { get; set; }

        /// <summary>Gets or sets a value indicating whether this object is deleted.</summary>
        ///
        /// <value>True if this object is deleted, false if not.</value>
        [JsonPropertyName("deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>Gets or sets the source for the.</summary>
        ///
        /// <value>The source.</value>
        [JsonPropertyName("source")]
        public Source Source { get; set; }

        /// <summary>Gets or sets a value indicating whether the used.</summary>
        ///
        /// <value>True if used, false if not.</value>
        [JsonPropertyName("used")]
        public bool Used { get; set; }

        /// <summary>Gets or sets the type.</summary>
        ///
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}