// <copyright file="FileFactLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the file fact logger class</summary>
namespace SampleCode
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>A file fact logger.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    ///
    /// <seealso cref="IAnimalFactLogger"/>
    public class FileFactLogger : IAnimalFactLogger
    {
        /// <summary>Full pathname of the file.</summary>
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFactLogger"/> class.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="filePath">Full pathname of the file.</param>
        public FileFactLogger(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>Writes an asynchronous.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="timestamp"> The timestamp Date/Time.</param>
        /// <param name="animalType">Type of the animal.</param>
        /// <param name="fact">      The fact.</param>
        ///
        /// <returns>An asynchronous result.</returns>
        public async Task WriteAsync(DateTime timestamp, string animalType, string fact)
        {
            var msg = $"{timestamp.ToUniversalTime():o}\t{animalType}\t{fact}\r\n";
            await File.AppendAllTextAsync(this.filePath, msg);
        }
    }
}
