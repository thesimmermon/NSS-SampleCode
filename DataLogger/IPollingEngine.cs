// <copyright file="IPollingEngine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Declares the IPollingEngine interface</summary>
namespace SampleCode
{
    using System;

    /// <summary>Interface for polling engine.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    public interface IPollingEngine : IDisposable
    {
        /// <summary>Starts the poller.</summary>
        ///
        /// <param name="config">The configuration.</param>
        void Start(PollingConfiguration config);

        /// <summary>Stops this object.</summary>
        void Stop();
    }
}
