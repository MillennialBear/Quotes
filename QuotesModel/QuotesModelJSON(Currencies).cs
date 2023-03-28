using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QuotesModelBase;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;

namespace QuotesModel
{
    public partial class QuotesModelJSON : ICurrencies
    {
        decimal rateCurrencyFrom = 0;
        decimal rateCurrencyTo = 0;
        decimal nominalCurrencyFrom = 0;
        decimal nominalCurrencyTo = 0;

        protected override ImmutableHashSet<CurrencyDto> GetCurrencies()
            => currenciesDTO.ToImmutable();

        protected override void UpdateCurrencies()
        {            
            using WebClient webclient = new WebClient();
            string jsonString = webclient.DownloadString(Source);

            JObject obj = JObject.Parse(jsonString);
            Dictionary<string, CurrencyJSON> newDictionary = JsonConvert.DeserializeObject<Dictionary<string, CurrencyJSON>>(obj["Valute"].ToString());
            foreach (var keyValue in newDictionary)
                keyValue.Value.CurrencyKey = keyValue.Key;

            currenciesJSON = newDictionary.ToDictionary(pair => pair.Key, pair => pair.Value);
            currenciesDTO.Clear();
            currenciesDTO.UnionWith(currenciesJSON.Select(json => CreateCurrencyDto(json.Value)));
            var currDTO = currenciesDTO.ToList();

            OnCurrenciesChanged(currDTO);            
        }

        protected override SearchResult SearchCode(string searchString)
        {            
            decimal valueRateUSDToRUB = currenciesDTO.FirstOrDefault(x => x.CurrencyKey == "USD").Value;
            if (!string.IsNullOrEmpty(searchString))
            {
                foreach (var currency in currenciesDTO)
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

        protected override void SetConvertCurrencies(CurrencyDto selectCurrencyFrom, CurrencyDto selectCurrencyTo)
        {
            if (selectCurrencyFrom != null)
            {
                rateCurrencyFrom = selectCurrencyFrom.Value;
                nominalCurrencyFrom = selectCurrencyFrom.Nominal;
            }
            if (selectCurrencyTo != null)
            {
                rateCurrencyTo = selectCurrencyTo.Value;
                nominalCurrencyTo = selectCurrencyTo.Nominal;
            }
        }
        
        protected override decimal ConvertFrom(decimal amountFrom)
        {
            if (rateCurrencyTo == 0)
                return 0;
            return Math.Round(rateCurrencyFrom / nominalCurrencyFrom / (rateCurrencyTo / nominalCurrencyTo) * amountFrom, 4, MidpointRounding.AwayFromZero);
        }

        protected override decimal ConvertTo(decimal amountTo)
        {
            if (rateCurrencyFrom == 0)
                return 0;
            else
                return Math.Round(rateCurrencyTo / nominalCurrencyTo / (rateCurrencyFrom / nominalCurrencyFrom) * amountTo, 4, MidpointRounding.AwayFromZero);
        }
    }
}
