using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace EonNext.Monitor.Core
{
    public partial class EonNextClient
    {
        public IGraphQLClient? GraphQLClient { get; set; }
        public IDateTimeProvider? DateTimeProvider { get; set; }

        private const string _endpoint = "https://api.eonnext-kraken.energy/v1/graphql/";

        public EonNextClient()
        {
            if (GraphQLClient == null)
                GraphQLClient = new GraphQLHttpClient(_endpoint, new NewtonsoftJsonSerializer());

            if (DateTimeProvider == null)
                DateTimeProvider = new DateTimeProvider();
        }
    }
}