using System;
using QuotesModel;
using QuotesModelBase;
using QuotesViewBase;
using QuotesViewModel;
using System.Windows;

namespace Quotes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>Модель</summary>
        private IQuotesModel model;
        /// <summary>ViewModel</summary>
        private MainViewModel viewModel;
        /// <summary>Главное окно</summary>
        private Window mainWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            model = new QuotesModelJSON();
            model = model ?? throw new ArgumentNullException(nameof(model));
            viewModel = new MainViewModel(model);
            viewModel.ExceptionEvent += ShowException;

            mainWindow = new Window()
            {                
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                MinHeight = 450,
                MinWidth = 750,
                Content = new MainUC(),
                DataContext = viewModel
            };

            mainWindow.Show();
            LoadModel("https://www.cbr-xml-daily.ru/daily_json.js");
        }

        /// <summary>Метод загрузки Модели</summary>
        /// <param name="source">Строка источника загрузки</param>
        private void LoadModel(string source)
        {
            try
            {
                model.LoadCurrencies(source);
                viewModel.SetCurrencies();
            }
            catch (Exception ex)
            {
                ShowException(this, nameof(LoadModel), ex);
            }
        }

        /// <summary>Метод вывода сообщения об исключении</summary>
        /// <param name="sender">Источник исключения</param>
        /// <param name="nameMetod">Метод в котором возникло исключение</param>
        /// <param name="exc">Параметры исключения</param>
        private void ShowException(object sender, string nameMetod, Exception exc)
            => MessageBox.Show($"{sender}.{nameMetod}\r\n{exc}\r\n{exc.Message}");
    }
}
