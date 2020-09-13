// <copyright file="IAnimalFactLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Declares the IAnimalFactLogger interface</summary>
namespace SampleCode
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface for animal fact logger.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    public interface IAnimalFactLogger
    {
        /// <summary>Writes an asynchronous.</summary>
        ///
        /// <param name="timestamp"> The timestamp Date/Time.</param>
        /// <param name="animalType">Type of the animal.</param>
        /// <param name="fact">      The fact.</param>
        ///
        /// <returns>void.</returns>
        Task WriteAsync(DateTime timestamp, string animalType, string fact);
    }
}
