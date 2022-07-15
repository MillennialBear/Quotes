using Quotes.Mvvm;
using System.ComponentModel;

namespace Quotes.Proxy
{
    internal class ProxyCurrencyInpc<Currency> : OnPropertyChangedClass
    {
        public Currency CurrencyDto { get; private set; }
        public void SetDto(Currency dto)
        {
            if (!Equals(CurrencyDto, dto))
            {
                CurrencyDto = dto;
                RaisePropertyChanged(null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
