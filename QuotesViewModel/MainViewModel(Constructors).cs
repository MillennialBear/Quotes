using QuotesModelBase;
using QuotesViewModelBase;

namespace QuotesViewModel
{
    /// <summary>Класс ViewModel для Модели работающей с валютами</summary>
    public partial class MainViewModel
    {
        public QuotesViewModelDD QuotesViewModel { get; } = new QuotesViewModelDD();

        /// <summary>Модель</summary>
        private readonly IQuotesModel model;

        /// <summary>Конструктор</summary>        
        /// <param name="model">Модель</param>
        public MainViewModel(IQuotesModel model)
        {
            this.model = model;
            model.CurrenciesChanged += Model_ChangedCurrencies;
        }        
    }
}