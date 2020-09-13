// <copyright file="AnimalFactsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Jim Simmermon</author>
// <date>9/12/2020</date>
// <summary>Implements the animal facts tests class</summary>
namespace SampleCode.UnitTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>An animal facts tests.</summary>
    ///
    /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
    public class AnimalFactsTests
    {
        /// <summary>The fact text.</summary>
        private readonly string factText;

        /// <summary>The fact list text.</summary>
        private readonly string factListText;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalFactsTests"/> class.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        public AnimalFactsTests()
        {
            this.factText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "animalfact.json"));
            this.factListText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "animalfacts.json"));
        }

        /// <summary>Should return single fact with mock.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        [Fact]
        public async void ShouldReturnSingleFactWithMock()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var httpClient = this.GetMockHttpClient(handlerMock, this.factText);
            using var animalFactsEndpoint = new AnimalFacts(httpClient);

            var fact = await animalFactsEndpoint.GetRandomFactAsync();

            Assert.NotNull(fact);
            Assert.Equal("factId", fact.Id);
            Assert.Equal("userId", fact.UserId);
            Assert.Equal("Test Text.", fact.Text);
            Assert.Equal(1, fact.Version);
            Assert.False(fact.Used);
            Assert.False(fact.IsDeleted);

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }

        /// <summary>Should return fact list with mock.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        [Fact]
        public async void ShouldReturnFactListWithMock()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var httpClient = this.GetMockHttpClient(handlerMock, this.factListText);
            using var animalFactsEndpoint = new AnimalFacts(httpClient);

            var facts = await animalFactsEndpoint.GetRandomFactsAsync(amount: 2);
            var factList = facts.ToList();

            Assert.Equal(2, factList.Count);

            for (int i = 0; i < 2; i++)
            {
                var fact = factList[i];
                Assert.NotNull(fact);
                Assert.Equal($"factId{i + 1}", fact.Id);
                Assert.Equal($"userId{i + 1}", fact.UserId);
                Assert.Equal($"Test Text: {i + 1}.", fact.Text);
                Assert.Equal(i + 1, fact.Version);
                Assert.False(fact.Used);
                Assert.False(fact.IsDeleted);
            }

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }

        /// <summary>Should return single fact live.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="animal">The animal.</param>
        [Theory]
        [InlineData("cat")]
        [InlineData("dog")]
        [InlineData("horse")]
        public async void ShouldReturnSingleFactLive(string animal)
        {
            using var animalFactsEndpoint = new AnimalFacts();

            var fact = await animalFactsEndpoint.GetRandomFactAsync(animal: animal);

            Assert.NotNull(fact);
            Assert.NotNull(fact.Id);
            Assert.NotNull(fact.UserId);
            Assert.NotNull(fact.Text);
            Assert.Equal(animal, fact.Type);
        }

        /// <summary>Should return fact list live.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="animal">The animal.</param>
        /// <param name="amount">The amount.</param>
        [Theory]
        [InlineData("cat", 2)]
        [InlineData("horse", 10)]
        [InlineData("dog", 5)]
        public async void ShouldReturnFactListLive(string animal, int amount)
        {
            using var animalFactsEndpoint = new AnimalFacts();

            var facts = await animalFactsEndpoint.GetRandomFactsAsync(animal: animal, amount: amount);
            var factList = facts.ToList();

            Assert.Equal(amount, factList.Count);

            for (int i = 0; i < amount; i++)
            {
                var fact = factList[i];
                Assert.NotNull(fact);
                Assert.NotNull(fact.Id);
                Assert.NotNull(fact.UserId);
                Assert.NotNull(fact.Text);
                Assert.Equal(animal, fact.Type);
            }
        }

        /// <summary>Gets mock HTTP client.</summary>
        ///
        /// <remarks>Jim Simmermon, 9/12/2020.</remarks>
        ///
        /// <param name="handlerMock">The handler mock.</param>
        /// <param name="jsonText">   The JSON text.</param>
        ///
        /// <returns>The mock HTTP client.</returns>
        private HttpClient GetMockHttpClient(Mock<HttpMessageHandler> handlerMock, string jsonText)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonText),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            return new HttpClient(handlerMock.Object);
        }
    }
}
