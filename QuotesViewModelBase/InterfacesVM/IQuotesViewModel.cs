using System;
using System.ComponentModel;

namespace QuotesViewModelBase.InterfacesVM
{
    /// <summary>Делегат события возникновения исключения</summary>
    /// <param name="sender">Источник исключения</param>
    /// <param name="nameMetod">Метод где возникло исключение</param>
    /// <param name="exc">Параметры исключения</param>
    public delegate void ExceptionHandler(object sender, string nameMetod, Exception exc);

    /// <summary>Интерфейс ViewModel</summary>
    public interface IQuotesViewModel : ICurrenciesVM, INotifyPropertyChanged
    {
        /// <summary>Событие с сообщением об ошибке</summary>
        event ExceptionHandler ExceptionEvent;
    }
}