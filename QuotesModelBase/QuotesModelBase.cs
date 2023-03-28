using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Documents;

namespace QuotesModelBase
{
    public abstract class QuotesModelBase : IQuotesModel
    {

        public event CurrenciesChangedHandler CurrenciesChanged;

        /// <summary>Вспомогательный метод вызова события после обновления валют</summary>
        /// <param name="currencies">Обновленные валюты</param>
        protected void OnCurrenciesChanged(List<CurrencyDto> currencies) => CurrenciesChanged?.Invoke(this, ActionChanged.Updated, currencies);

        public ImmutableHashSet<CurrencyDto> GetCurrenciesApp() => GetCurrencies();

        /// <summary>Возвращает все валюты</summary>
        /// <returns>Множество валют</returns>
        protected abstract ImmutableHashSet<CurrencyDto> GetCurrencies();

        public void LoadCurrencies(string source) => Load(source);

        /// <summary>Загрузка данных</summary>
        /// <param name="source">Источник с данными</param>
        protected virtual void Load(string source)
        {            
            Source = source;
        }

        public string Source { get; protected set; } = null;
                
        public void ExchangeRateUpdate() => UpdateCurrencies();

        /// <summary>Обновление данных</summary>
        protected abstract void UpdateCurrencies();

        public SearchResult Search(string searchStr) => SearchCode(searchStr);

        /// <summary>Поиск по коду валюты</summary>
        protected abstract SearchResult SearchCode(string searchString);               

        public void SettingConvertibleCurrencies(CurrencyDto currencyFrom, CurrencyDto currencyTo) => SetConvertCurrencies(currencyFrom, currencyTo);

        /// <summary>Устанавливает валюты конвертации</summary>
        protected abstract void SetConvertCurrencies(CurrencyDto currencyFrom, CurrencyDto currencyTo);

        public decimal ConvertCurrencyFrom(decimal amountCurrency) => ConvertFrom(amountCurrency);

        /// <summary>Конвертация валюты "ИЗ</summary>
        protected abstract decimal ConvertFrom(decimal amountCurrencyFrom);

        public decimal ConvertCurrencyTo(decimal amountCurrency) => ConvertTo(amountCurrency);

        /// <summary>Конвертаци валюты "В"</summary>
        protected abstract decimal ConvertTo(decimal amountCurrencyTo);        
    }
}
