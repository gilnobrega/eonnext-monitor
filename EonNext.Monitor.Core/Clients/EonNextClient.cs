namespace EonNext.Monitor.Core
{
    public partial class EonNextClient
    {
        public IDateTimeProvider? DateTimeProvider { get; set; }

        private const string _endpoint = "https://api.eonnext-kraken.energy/v1/graphql/";
    }
}