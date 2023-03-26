using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuotesModelBase;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;

namespace QuotesModel
{
    /// <summary>Модель для работы с JSON файлом</summary>
    public partial class QuotesModelJSON : QuotesModelBase.QuotesModelBase
    {        
        /// <summary>Словарь десериализованных валют с ключом по CurrencyKey</summary>
        protected Dictionary<string, CurrencyJSON> currenciesJSON;

        /// <summary>Множество валют для ViewModel</summary>       
        protected readonly ImmutableHashSet<CurrencyDto>.Builder currenciesDTO = ImmutableHashSet.CreateBuilder<CurrencyDto>();

        /// <summary>Создаёт экземпляр DTO валюты из JSON типа</summary>
        /// <param name="currency">Экземпляр JSON типа</param>
        /// <returns>CurrencyDto</returns>
        protected static CurrencyDto CreateCurrencyDto(CurrencyJSON currency)
            => new CurrencyDto(currency.CurrencyKey, currency.ID, currency.NumCode, currency.CharCode,
                currency.Nominal, currency.Name, currency.Value, currency.Previous);
                
        /// <summary>Сравнивает все значения двух экземпляров</summary>
        /// <param name="source">Экземпляр DTO типа</param>
        /// <param name="target">Экземпляр JSON типа</param>
        /// <returns><see langword="true"/> если все значения равны</returns>
        protected static bool EqualsCurrency(CurrencyDto source, CurrencyJSON target)
            => target.CurrencyKey == source.CurrencyKey && target.ID == source.ID && target.NumCode == source.NumCode
            && target.CharCode == source.CharCode && target.Nominal == source.Nominal && target.Name == source.Name
            && target.Value == source.Value && target.Previous == source.Previous;
                
        protected override void Load(string source)
        {
            try
            {                
                using WebClient webclient = new WebClient();
                string jsonString = webclient.DownloadString(source);

                JObject obj = JObject.Parse(jsonString);
                Dictionary<string, CurrencyJSON> newDictionary = JsonConvert.DeserializeObject<Dictionary<string, CurrencyJSON>>(obj["Valute"].ToString());                
                foreach (var keyValue in newDictionary)                
                    keyValue.Value.CurrencyKey = keyValue.Key;                
                
                currenciesJSON = newDictionary.ToDictionary(pair => pair.Key, pair => pair.Value);
                currenciesDTO.Clear();
                currenciesDTO.UnionWith(currenciesJSON.Select(json => CreateCurrencyDto(json.Value)).ToImmutableHashSet());                

                base.Load(source);
            }
            catch (Exception ex)
            {
                throw new QuotesModelException($"Ошибка загрузки JSON \"{source}\"", QuotesModelExceptionEnum.LoadingData, ex);
            }
        }        
    }
}
