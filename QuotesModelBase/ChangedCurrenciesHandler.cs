using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Documents;

namespace QuotesModelBase
{
    /// <summary>Делегат события изменения Валют</summary>
    /// <param name="sender">Объект где возникло событие</param>
    /// <param name="action">Какое было изменение</param>
    /// <param name="currencies">Список изменённых Валют</param>    
    public delegate void CurrenciesChangedHandler(object sender, ActionChanged action, List<CurrencyDto> currencies);
}
