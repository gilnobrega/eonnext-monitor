using Xunit;
using Moq;
using FluentAssertions;
using EonNext.Monitor.Core;
using GraphQL.Client.Abstractions;
using GraphQL;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EonNext.Monitor.Test
{
    public class EonNextClientTest
    {
        Mock<IGraphQLClient> graphQLClientMock;

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
    }
}