// <copyright file="PollingEngineTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/13/2020</date>
// <summary>Implements the polling engine tests class</summary>
namespace SampleCode.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    /// <summary>A polling engine tests.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
    public class PollingEngineTests
    {
        /// <summary>Tests polling interval.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/13/2020.</remarks>
        [Fact]
        public void TestPollingInterval()
        {
            var loggerMock = new Mock<IAnimalFactLogger>();
            loggerMock.Setup(l => l.WriteAsync(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            var logger = loggerMock.Object;

            var animalFactsMock = new Mock<IAnimalFacts>();
            animalFactsMock.Setup(a => a.GetRandomFactAsync(It.IsAny<string>())).ReturnsAsync(() => new AnimalFact()).Verifiable();
            var facts = animalFactsMock.Object;

            var poller = new PollingEngine(logger, facts);
            poller.Start(new PollingConfiguration { Amount = 1, Animal = "cat", Interval = 1 });

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            poller.Stop();

            loggerMock.Verify(l => l.WriteAsync(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeast(2));
            animalFactsMock.Verify(a => a.GetRandomFactAsync(It.IsAny<string>()), Times.AtLeast(2));
        }
    }
}
