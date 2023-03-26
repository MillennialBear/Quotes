
namespace QuotesModelBase
{
    public interface IQuotesModel : ICurrencies
    {
        /// <summary>Загрузка данных</summary>
        /// <param name="source">Источник с данными</param>
        void LoadCurrencies(string sourse);

        /// <param name="source">Источник данных</param>
        string Source { get; }
    }
}
