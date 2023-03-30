using Common;

namespace QuotesViewModelBase.InterfacesVM
{
    /// <summary>Интерфейс элемента Валюты.</summary>
    public interface ICurrencyVM
    {
        string CurrencyKey { get; set; }
        string ID { get; set; }
        int NumCode { get; set; }
        string CharCode { get; set; }
        decimal Nominal { get; set; }
        string Name { get; set; }
        decimal Value { get; set; }
        decimal Previous { get; set; }        
    }
}
