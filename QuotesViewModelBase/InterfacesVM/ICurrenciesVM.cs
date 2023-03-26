using Common;
using System.Collections.ObjectModel;

namespace QuotesViewModelBase.InterfacesVM
{
    /// <summary>Интерфейс ViewModel</summary>
    public interface ICurrenciesVM
    {
        /// <summary>Коллекция валют</summary>
        ObservableCollection<ICurrencyVM> Currencies { get; }
                
        /// <summary>Команда поиска по коду валюты</summary>
        RelayCommand SearchCodeCommand { get; }

        /// <summary>Команда обновления валют</summary>
        RelayCommand UpdateCurrenciesCommand { get; }        
    }
}