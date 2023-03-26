using Common;

namespace QuotesModelBase
{
    public class CurrencyDto : BaseIdDTO
    {
        public new string CurrencyKey { get; }

        public string ID { get; }

        public int NumCode { get; }

        public string CharCode { get; }

        public decimal Nominal { get; }

        public string Name { get; }

        public decimal Value { get; }

        public decimal Previous { get; }

        public CurrencyDto(string currencyKey, string id, int numCode, string charCode,
            decimal nominal, string name, decimal value, decimal previous) : base(currencyKey)
        {
            CurrencyKey = currencyKey;
            ID = id;
            NumCode = numCode;
            CharCode = charCode;
            Nominal = nominal;
            Name = name;
            Value = value;
            Previous = previous;
        }
    }
}
