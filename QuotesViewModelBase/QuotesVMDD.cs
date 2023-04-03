using Common;
using QuotesViewModelBase.InterfacesVM;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace QuotesViewModelBase
{
    public class QuotesVMDD : OnPropertyChangedClass, IQuotesViewModel
    {
        #region Поля для хранения значений свойств
        private ICurrencyVM selectCurrencyFrom;
        private ICurrencyVM selectCurrencyTo;
        private string convertValueFrom;
        private string convertValueTo;
        private string textMessage;
        private string textSearch;        
        #endregion

        #region Поля для хранения значений свойств-команд
        private RelayCommand searchCodeCommand;
        private RelayCommand updateCurrenciesCommand;
        #endregion

        #region Свойства для привязки элементов отображения
        public ObservableCollection<ICurrencyVM> Currencies { get; } = new ObservableCollection<ICurrencyVM>();        
        public ICurrencyVM SelectCurrencyFrom { get => selectCurrencyFrom; set => SetProperty(ref selectCurrencyFrom, value); }
        public ICurrencyVM SelectCurrencyTo { get => selectCurrencyTo; set => SetProperty(ref selectCurrencyTo, value); }
        public string ConvertValueFrom { get => convertValueFrom; set => SetProperty(ref convertValueFrom, value); }
        public string ConvertValueTo { get => convertValueTo; set => SetProperty(ref convertValueTo, value); }
        public string TextMessage { get => textMessage; set => SetProperty(ref textMessage, value); }
        public string TextSearch { get => textSearch; set => SetProperty(ref textSearch, value); }
        #endregion

        #region Свойства для привязки команд
        public RelayCommand SearchCodeCommand => searchCodeCommand ?? (searchCodeCommand = new RelayCommand(CurrencySearchMethod));
        public RelayCommand UpdateCurrenciesCommand => updateCurrenciesCommand ?? (updateCurrenciesCommand = new RelayCommand(CurrenciesUpdateMethod));
        #endregion

        #region Методы для команд
        /// <summary>Метод для вызова из команды поиска</summary>
        /// <param name="Value">Значение привязанного параметра</param>        
        protected virtual void CurrencySearchMethod(object Value = null) { }

        /// <summary>Метод для вызова из команды обновления</summary>
        protected virtual void CurrenciesUpdateMethod(object Value = null) { }
        #endregion

        public event ExceptionHandler ExceptionEvent;
        public event InfoHandler InfoEvent;

        /// <summary>Вспомогательный метод для передачи сообщения об ошибке</summary>
        /// <param name="exc">Параметры ошибки</param>
        /// <param name="nameMetod">Метод отправивший сообщение</param>
        public void OnException(Exception exc, [CallerMemberName] string nameMetod = null)
            => ExceptionEvent?.Invoke(this, nameMetod, exc);

        /// <summary>Вспомогательный метод для передачи сообщения об обновлении валют</summary>       
        /// <param name="dt">Время обновления</param>
        public void OnInfo(DateTime dt) => InfoEvent?.Invoke(this, dt);
    }
}