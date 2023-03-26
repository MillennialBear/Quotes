using System.Collections.Immutable;

namespace QuotesModelBase
{
    public interface ICurrencies
    {
        /// <summary>Возвращает все валюты</summary>
        /// <returns>Множество валют</returns>
        ImmutableHashSet<CurrencyDto> GetCurrenciesApp();

        /// <summary>Поиск по коду валюты</summary>        
        SearchResult Search(string searchString);

        /// <summary>Устанавливает валюты конвертации</summary>   
        void SettingConvertibleCurrencies(CurrencyDto currencyFrom, CurrencyDto currencyTo);

        /// <summary>Обновление валют</summary>
        void ExchangeRateUpdate();

        /// <summary>Конвертация валюты "ИЗ"</summary>
        /// <param name="amountFrom">Количество валюты "ИЗ"</param>
        decimal ConvertCurrencyFrom(decimal amountFrom);

        /// <summary>Конвертация валюты "В"</summary>
        /// <param name="amountTo">Количество валюты "В"</param>
        decimal ConvertCurrencyTo(decimal amountTo);

        /// <summary>Событие о любых изменениях в коллекции валют</summary>
        event CurrenciesChangedHandler CurrenciesChanged;        
    }
}
