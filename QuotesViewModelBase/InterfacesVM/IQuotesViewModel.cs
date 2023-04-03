using System;
using System.ComponentModel;

namespace QuotesViewModelBase.InterfacesVM
{
    /// <summary>Делегат события возникновения исключения</summary>
    /// <param name="sender">Источник исключения</param>
    /// <param name="nameMetod">Метод где возникло исключение</param>
    /// <param name="exc">Параметры исключения</param>
    public delegate void ExceptionHandler(object sender, string nameMetod, Exception exc);

    /// <summary>Делегат события вывода информации об обновлении валют</summary>
    /// <param name="sender">Источник исключения</param>    
    /// <param name="dt">Время обновления</param>
    public delegate void InfoHandler(object sender, DateTime dt);

    /// <summary>Интерфейс ViewModel</summary>
    public interface IQuotesViewModel : ICurrenciesVM, INotifyPropertyChanged
    {
        /// <summary>Событие с сообщением об ошибке</summary>
        event ExceptionHandler ExceptionEvent;

        /// <summary>Событие с сообщением об обновлении валют</summary>
        event InfoHandler InfoEvent;
    }
}