// <copyright file="ConsoleFactLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the console fact logger class</summary>
namespace SampleCode
{
    using System;
    using System.Threading.Tasks;

    /// <summary>A console fact logger.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    ///
    /// <seealso cref="IAnimalFactLogger"/>
    public class ConsoleFactLogger : IAnimalFactLogger
    {
        /// <summary>The locker for writing to the console.</summary>
        private static readonly object Locker = new object();

        /// <summary>Writes an asynchronous.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="timestamp"> The timestamp Date/Time.</param>
        /// <param name="animalType">Type of the animal.</param>
        /// <param name="fact">      The fact.</param>
        ///
        /// <returns>void.</returns>
        public async Task WriteAsync(DateTime timestamp, string animalType, string fact)
        {
            await Task.Run(() =>
            {
                var msg = $"{timestamp.ToUniversalTime():o}\t{animalType}\t{fact}";

                switch (animalType.ToLowerInvariant())
                {
                    case "cat":
                        WriteToConsole(msg, ConsoleColor.Red);
                        break;

                    case "dog":
                        WriteToConsole(msg, ConsoleColor.Blue);
                        break;

                    case "horse":
                        WriteToConsole(msg, ConsoleColor.Green);
                        break;

                    default:
                        WriteToConsole(msg, ConsoleColor.Gray);
                        break;
                }
            });
        }

        /// <summary>
        ///     Writes to console using a lock object to ensure the color doesn't get mangled.
        ///     Might be better to queue this, in the future.
        /// </summary>
        ///
        /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
        ///
        /// <param name="msg">  The message.</param>
        /// <param name="color">The color.</param>
        private static void WriteToConsole(string msg, ConsoleColor color)
        {
            lock (Locker)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(msg);
                Console.ResetColor();
            }
        }
    }
}
