using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Quotes
{
    public class СurrenciesServiceModel
    {
        public Dictionary<string, Currency> DictСurrencies { get; private set; } = new Dictionary<string, Currency>();
        public event EventHandler<Dictionary<string, Currency>> СurrenciesInitialized;
        List<Currency> listСurrencies;

        public List<Currency> ListСurrencies
        {
            get => listСurrencies ?? (listСurrencies = new List<Currency>());
            private set => listСurrencies = value;
        }

        public void OutputJson()
        {
            string url = "https://www.cbr-xml-daily.ru/daily_json.js";
            using WebClient webclient = new WebClient();
            string jsonString = webclient.DownloadString(url);

            JObject obj = JObject.Parse(jsonString);
            Dictionary<string, Currency> newDictionary = JsonConvert.DeserializeObject<Dictionary<string, Currency>>(obj["Valute"].ToString());
            DictСurrencies = newDictionary;

            СurrenciesInitialized?.Invoke(this, newDictionary);
        }

        public void СurrenciesInitizalized(object? sender, Dictionary<string, Currency> e)
        {
            ListСurrencies = e.Select(currencyPair =>
                new Currency
                {
                    CurrencyKey = currencyPair.Key,
                    ID = currencyPair.Value.ID,
                    NumCode = currencyPair.Value.NumCode,
                    CharCode = currencyPair.Value.CharCode,
                    Nominal = currencyPair.Value.Nominal,
                    Name = currencyPair.Value.Name,
                    Value = currencyPair.Value.Value,
                    Previous = currencyPair.Value.Previous
                }).ToList();
        }
        
        decimal rateCurrencyFrom = 0;
        decimal rateCurrencyTo = 0;
        decimal nominalCurrencyFrom = 0;
        decimal nominalCurrencyTo = 0;

        public void SetCurrencyFrom(Currency selectCurrency)
        {
            if (selectCurrency != null)
            {
                rateCurrencyFrom = selectCurrency.Value;
                nominalCurrencyFrom = selectCurrency.Nominal;
            }
        }

        public void SetCurrencyTo(Currency selectCurrency)
        {
            if (selectCurrency != null)
            {
                rateCurrencyTo = selectCurrency.Value;
                nominalCurrencyTo = selectCurrency.Nominal;
            }
        }

        public decimal ConvertCurrencyFrom(decimal amount)
        {
            if (rateCurrencyTo == 0)
                return 0;
            return (Math.Round((rateCurrencyFrom / nominalCurrencyFrom) / (rateCurrencyTo / nominalCurrencyTo) * amount, 4, MidpointRounding.AwayFromZero));
        }

        public decimal ConvertCurrencyTo(decimal amount)
        {
            if (rateCurrencyFrom == 0)
                return 0;
            else
                return (Math.Round((rateCurrencyTo / nominalCurrencyTo) / (rateCurrencyFrom / nominalCurrencyFrom) * amount, 4, MidpointRounding.AwayFromZero));
        }

        public SearchResult SearchCode(string searchString)
        {            
            decimal valueRateUSDToRUB = ListСurrencies.FirstOrDefault(x => x.CurrencyKey == "USD").Value;            
            foreach (var currency in ListСurrencies)
            {
                if (searchString != null)
                {
                    if (currency.CurrencyKey.Contains(searchString.ToUpper()))
                    {
                        return new SearchResult
                        {
                            Type = ResultType.ExactMatch,
                            NameOfCurrency = currency.Name,
                            NominalOfCurrency = currency.Nominal,
                            ValueRateToRUB = currency.Value,
                            ValueRateToUSD = Math.Round(currency.Value / valueRateUSDToRUB, 4, MidpointRounding.AwayFromZero)
                        };
                    }
                }
            }

            return new SearchResult
            {
                Type = ResultType.NoMatch,
                NameOfCurrency = "",
                NominalOfCurrency = -1,
                ValueRateToRUB = -1
            };
        }

        public class SearchResult
        {
            public ResultType Type;
            public decimal NominalOfCurrency;
            public decimal ValueRateToRUB;
            public decimal ValueRateToUSD;
            public string NameOfCurrency;
        }
                
        public enum ResultType
        {
            ExactMatch,
            NoMatch
        }
    }
}