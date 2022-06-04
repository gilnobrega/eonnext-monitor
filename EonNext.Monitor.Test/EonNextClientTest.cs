using Xunit;
using Moq;
using FluentAssertions;
using EonNext.Monitor.Core;
using System.Collections.Generic;
using System;
using GraphQL.Client.Abstractions;
using GraphQL;
using System.Threading.Tasks;

namespace EonNext.Monitor.Test
{
    public class EonNextClientTest
    {
        Mock<IGraphQLClient> graphQLClientMock;

        EonNextClientTest()
        {
        }

        [Fact]
        public void Should_ReturnFullNameAndAccountNumber()
        {

            graphQLClientMock = new Mock<IGraphQLClient>();
            graphQLClientMock.Setup(client => client.SendQueryAsync<dynamic>(EonNextClient.accountInfoRequest, default)).Returns(Task.FromResult(new GraphQLResponse<dynamic>()
            {
                Data = {
                    fullName = "Joe Smith",
                    accounts = new List<dynamic> {
                        new {
                            number = "A1234"
                        }
                    }
                }
            }));

            EonNextClient client = new EonNextClient();

            (string fullName, string accountNumber) = client.GetFullNameAndAccountNumber();

            fullName.Should().Be("Joe Smith");
            accountNumber.Should().Be("A1234");
        }
    }
}