using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quotes
{
    internal class СurrenciesServiceVMDataDesigner : OnPropertyChangedClass
    {
        #region Свойства для привязки элементов отображения              
        public List<Currency> ListCurrency { get => listCurrency; set { listCurrency = value; OnAllPropertyChanged(); } }
        public Currency SelectCurrencyFrom { get => selectCurrencyFrom; set { selectCurrencyFrom = value; OnAllPropertyChanged(); } }
        public Currency SelectCurrencyTo { get => selectCurrencyTo; set { selectCurrencyTo = value; OnAllPropertyChanged(); } }
        public string ConvertValueFrom { get => convertValueFrom; set => SetProperty(ref convertValueFrom, value); }
        public string ConvertValueTo { get => convertValueTo; set => SetProperty(ref convertValueTo, value); }
        public string TextMessage { get => textMessage; set { textMessage = value; OnAllPropertyChanged(); } }
        public string TextSearch { get => textSearch; set { textSearch = value; OnAllPropertyChanged(); } }
        #endregion

        #region Приватные поля
        // Поля для хранения значения свойства
        private List<Currency> listCurrency;
        private Currency selectCurrencyFrom;
        private Currency selectCurrencyTo;
        private string convertValueFrom;
        private string convertValueTo;
        private string textMessage;
        private string textSearch;

        // Поля для хранения значения команд
        private ICommand searchComm;
        private ICommand updateComm;
        #endregion

        #region Свойства для привязки команд
        /// <summary>Свойство для привязки команды</summary>
        public ICommand SearchComm => searchComm ?? (searchComm = new RelayCommand(OnSearch));
        public ICommand UpdateComm => updateComm ?? (updateComm = new RelayCommand(OnUpdate));
        #endregion

        #region Методы для команд
        /// <summary>Метод для вызова из команды</summary>
        /// <param name="Value">Значение привязанного параметра</param>        
        public virtual void OnSearch(object Value = null) { }
        public virtual void OnUpdate(object Value = null) { }
        #endregion

    }
}
