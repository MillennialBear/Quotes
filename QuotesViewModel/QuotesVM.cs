using QuotesModelBase;
using QuotesViewModelBase;
using QuotesViewModelBase.VMClasses;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows;

namespace QuotesViewModel
{
    public partial class QuotesVM : QuotesVMDD
    {
        /// <summary>Блокировка обновления ViewModel</summary>   
        bool blockViewModelUpdates;

        /// <summary>Обработчик события изменения в валютах</summary>
        /// <param name="sender">Источник события</param>
        /// <param name="action">Действие события</param>
        /// <param name="currencies">Валюты затронутые событием</param>
        private void Model_ChangedCurrencies(object sender, ActionChanged action, List<CurrencyDto> currencies)
        {
            switch (action)
            {
                case ActionChanged.Updated:
                    if(currencies.Any())                        
                        ChangingCurrencies(currencies);                    
                    break;                                    
                default:
                    throw new ArgumentException(null, nameof(action));
            }
        }

        /// <summary>Изменение Валют</summary>
        /// <param name="currencies">Изменяемые Валюты</param>
        private void ChangingCurrencies(List<CurrencyDto> currencies)
        {            
            /// Создание коллекции изменяемых Валют
            Dictionary<CurrencyDto, CurrencyVM> list = new Dictionary<CurrencyDto, CurrencyVM>(currencies.Count);

            foreach (var curr in currencies)
            {                
                CurrencyVM? cur = (CurrencyVM?)Currencies.FirstOrDefault(d => d.CurrencyKey == curr.CurrencyKey);
                if(cur is null || cur.EqualValues(curr))
                    continue;                
                cur.CopyFromDTO(curr);
            }

            DateTime dt = DateTime.Now;
            OnInfo(dt);
        }

        /// <summary>Инициализация коллекции валют</summary>        
        public virtual void SetCurrencies()
        {
            ImmutableHashSet<CurrencyDto> currencies = model.GetCurrenciesApp();
            
            int index = 0;
            foreach (CurrencyDto currency in currencies)
            {
                if (index < Currencies.Count)
                    ((CurrencyVM)Currencies[index]).CopyFromDTO(currency);
                else
                    Currencies.Add(new CurrencyVM(currency));
                index++;
            }            
        }

        /// <summary>Метод изменения Валют</summary>        
        protected override void CurrenciesUpdateMethod(object Value = null)
        {
            try { model.ExchangeRateUpdate(); }
            catch (Exception ex) { OnException(ex); }            
        }

        /// <summary>Метод поиска валюты</summary>        
        protected override void CurrencySearchMethod(object Value = null)
        {
            try
            {
                SearchResult res = model.Search(TextSearch);
                switch (res.Type)
                {
                    case ResultType.ExactMatch:
                        TextMessage = res.NominalOfCurrency + " "
                            + res.NameOfCurrency + " = " + res.ValueRateToRUB
                            + " ₽" + "\n" + res.NominalOfCurrency + " "
                            + res.NameOfCurrency + " = " + res.ValueRateToUSD
                            + " $"; break;
                    default: TextMessage = "Введите код валюты. Например: \"AZN\"."; break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Конвертация валют в представлении        
        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            CurrencyDto currencyDtoFrom = null;
            if (SelectCurrencyFrom != null)
            {
                CurrencyVM currencyVMFrom = new();
                currencyVMFrom.CopyFrom(SelectCurrencyFrom);
                currencyDtoFrom = currencyVMFrom.CopyDTO();
            }

            CurrencyDto currencyDtoTo = null;
            if (SelectCurrencyTo != null)
            {
                CurrencyVM currencyVMTo = new();
                currencyVMTo.CopyFrom(SelectCurrencyTo);
                currencyDtoTo = currencyVMTo.CopyDTO();
            }

            if (propertyName == nameof(ConvertValueFrom))
            {
                if (blockViewModelUpdates)
                    return;
                model.SettingConvertibleCurrencies(currencyDtoFrom, currencyDtoTo);
                
                decimal amountCurrency = -1;
                if (decimal.TryParse(ConvertValueFrom, out amountCurrency))
                {
                    blockViewModelUpdates = true;
                    ConvertValueTo = model.ConvertCurrencyFrom(amountCurrency).ToString();
                    blockViewModelUpdates = false;
                }
            }
            if (propertyName == nameof(ConvertValueTo))
            {
                if (blockViewModelUpdates)
                    return;
                model.SettingConvertibleCurrencies(currencyDtoFrom, currencyDtoTo);

                decimal amountCurrency = -1;
                if (decimal.TryParse(ConvertValueTo, out amountCurrency))
                {
                    blockViewModelUpdates = true;
                    ConvertValueFrom = model.ConvertCurrencyTo(amountCurrency).ToString();
                    blockViewModelUpdates = false;
                }
            }
        }
    }
}
