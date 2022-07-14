using Quotes.Services;
using System;
using System.Windows;
using static Quotes.Services.СurrenciesService;

namespace Quotes.ViewModels
{
    internal class СurrenciesServiceVM : СurrenciesServiceVMDataDesigner
    {
        private СurrenciesService model;
        bool blockViewModelUpdates;

        public СurrenciesServiceVM()
        {
            model = new СurrenciesService();
            model.СurrenciesInitialized += model.СurrenciesInitizalized;
            model.OutputJson();
            ListCurrency = model.ListСurrencies;
        }

        public override void OnSearch(object Value = null)
        {
            try
            {
                SearchResult res = model.SearchCode(TextSearch);
                switch (res.Type)
                {
                    case ResultType.ExactMatch:
                        TextMessage = res.NominalOfCurrency + " "
                            + res.NameOfCurrency + " = " + res.ValueRateToRUB
                            + " руб." + "\n" + res.NominalOfCurrency + " "
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

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            if (propertyName == nameof(ConvertValueFrom))
            {
                if (blockViewModelUpdates)
                    return;
                model.SetCurrencyFrom(SelectCurrencyFrom);
                model.SetCurrencyTo(SelectCurrencyTo);
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
                model.SetCurrencyFrom(SelectCurrencyFrom);
                model.SetCurrencyTo(SelectCurrencyTo);
                decimal amountCurrency = -1;
                if (decimal.TryParse(ConvertValueTo, out amountCurrency))
                {
                    blockViewModelUpdates = true;
                    ConvertValueFrom = model.ConvertCurrencyFrom(amountCurrency).ToString();
                    blockViewModelUpdates = false;
                }
            }
        }

        //public override void OnUpdate(object Value = null)
        //{
        //    model.OutputJson();
        //    ListCurrency = model.ListСurrencies;
        //}
    }
}
