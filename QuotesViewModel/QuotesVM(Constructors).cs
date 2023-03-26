using QuotesModelBase;
using QuotesViewModelBase;

namespace QuotesViewModel
{
    /// <summary>Класс ViewModel для Модели работающей с валютами</summary>
    public partial class QuotesVM
    {
        /// <summary>Модель</summary>
        private readonly IQuotesModel model;

        /// <summary>Конструктор</summary>        
        /// <param name="model">Модель</param>
        public QuotesVM(IQuotesModel model)
        {
            this.model = model;
            model.CurrenciesChanged += Model_ChangedCurrencies;
        }        
    }
}