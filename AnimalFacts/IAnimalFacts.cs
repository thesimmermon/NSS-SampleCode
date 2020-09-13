// <copyright file="IAnimalFacts.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Declares the IAnimalFacts interface</summary>
namespace SampleCode
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Interface for animal facts.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    public interface IAnimalFacts : IDisposable
    {
        /// <summary>Gets random facts.</summary>
        ///
        /// <param name="animal">(Optional) The animal.</param>
        /// <param name="amount">(Optional) Number of facts to return.</param>
        ///
        /// <returns>The random facts.</returns>
        Task<IEnumerable<AnimalFact>> GetRandomFactsAsync(string animal = null, int? amount = null);

        /// <summary>Gets random fact.</summary>
        ///
        /// <param name="animal">(Optional) The animal.</param>
        ///
        /// <returns>The random fact.</returns>
        Task<AnimalFact> GetRandomFactAsync(string animal = null);
    }
}
