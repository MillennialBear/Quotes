
namespace QuotesModelBase
{
    public class SearchResult
    {
        public ResultType Type { get; set; }

        public decimal NominalOfCurrency { get; set; }

        public decimal ValueRateToRUB { get; set; }

        public decimal ValueRateToUSD { get; set; }

        public string NameOfCurrency { get; set; }
    }

    public enum ResultType
    {
        ExactMatch,
        NoMatch
    }
}
