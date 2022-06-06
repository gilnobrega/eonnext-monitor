using Xunit;
using Moq;
using FluentAssertions;
using EonNext.Monitor.Core;
using GraphQL.Client.Abstractions;
using GraphQL;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

namespace EonNext.Monitor.Test
{
    public class EonNextClientTest
    {
        Mock<IGraphQLClient> graphQLClientMock;
        Mock<IDateTimeProvider> dateTimeProviderMock;

        public EonNextClientTest()
        {
            graphQLClientMock = new Mock<IGraphQLClient>();
            dateTimeProviderMock = new Mock<IDateTimeProvider>();

            dateTimeProviderMock.Setup(provider => provider.GetCurrentDate()).Returns(DateTime.Parse("01/01/2022 00:01"));
        }

        [Fact]
        public async void Should_ReturnFullNameAndAccountNumber()
        {
            graphQLClientMock = new Mock<IGraphQLClient>();
            graphQLClientMock.Setup(client => client.SendQueryAsync<JObject>(EonNextClient.accountInfoRequest, default)).Returns(Task.FromResult(new GraphQLResponse<JObject>()
            {
                Data = new JObject(new JProperty("viewer", new JObject(new JProperty("fullName", "Joe Smith"), new JProperty("accounts", new JArray(new JObject(new JProperty("number", "A1234")))))))
            }));

            EonNextClient client = new EonNextClient
            {
                GraphQLClient = graphQLClientMock.Object
            };

            (string fullName, string accountNumber) = await client.GetFullNameAndAccountNumber();

            fullName.Should().Be("Joe Smith");
            accountNumber.Should().Be("A1234");
        }

        [Fact]
        public async void Should_ReturnValidLoginThenLogout_WhenTokenNotExpired()
        {
            var expirationDate = DateTime.Parse("2022/01/01 00:01:01");
            graphQLClientMock = new Mock<IGraphQLClient>();
            graphQLClientMock.Setup(client => client.SendQueryAsync<JObject>(EonNextClient.GenerateAuthenticationTokenRequest("Test", "1234"), default)).Returns(Task.FromResult(new GraphQLResponse<JObject>()
            {
                Data = new JObject(new JProperty("obtainKrakenToken", new JObject(new JProperty("token", "testToken"), new JProperty("refreshExpiresIn", ((DateTimeOffset)expirationDate).ToUnixTimeSeconds()))))
            }));

            EonNextClient client = new EonNextClient
            {
                GraphQLClient = graphQLClientMock.Object,
                DateTimeProvider = dateTimeProviderMock.Object
            };

            await client.Login("Test", "1234");

            client.AuthenticationToken.Should().Be("testToken");
            client.TokenExpirationDate.Should().Be(expirationDate);

            client.IsLoggedIn().Should().BeTrue();

            client.Logout();
            client.AuthenticationToken.Should().Be(null);
            client.TokenExpirationDate.Should().Be(null);

            client.IsLoggedIn().Should().BeFalse();
        }

        [Fact]
        public async void Should_ReturnInvalidLogin_WhenTokenExpired()
        {
            var expirationDate = DateTime.Parse("2021/12/31 23:59:59");
            graphQLClientMock = new Mock<IGraphQLClient>();
            graphQLClientMock.Setup(client => client.SendQueryAsync<JObject>(EonNextClient.GenerateAuthenticationTokenRequest("Test", "1234"), default)).Returns(Task.FromResult(new GraphQLResponse<JObject>()
            {
                Data = new JObject(new JProperty("obtainKrakenToken", new JObject(new JProperty("token", "testToken"), new JProperty("refreshExpiresIn", ((DateTimeOffset)expirationDate).ToUnixTimeSeconds()))))
            }));

            EonNextClient client = new EonNextClient
            {
                GraphQLClient = graphQLClientMock.Object,
                DateTimeProvider = dateTimeProviderMock.Object
            };

            await client.Login("Test", "1234");

            client.AuthenticationToken.Should().Be("testToken");
            client.TokenExpirationDate.Should().Be(expirationDate);

            client.IsLoggedIn().Should().BeFalse();
        }
    }
}