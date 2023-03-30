using Common;
using QuotesModelBase;
using QuotesViewModelBase.InterfacesVM;

namespace QuotesViewModelBase.VMClasses
{
    /// <summary>Класс VM для валют работающий с DTO типом CurrencyDto</summary>    
    public class CurrencyVM : BaseIdVM<CurrencyDto>, ICurrencyVM
    { 
        #region Поля для хранения начений свойств
        private string currencyKey;
        private string id;
        private int numCode;
        private string charCode;
        private decimal nominal;
        private string name;
        private decimal valueCurrency;
        private decimal previous;
        #endregion

        #region Свойства
        public string CurrencyKey { get => currencyKey; set => SetProperty(ref currencyKey, value); }
        public string ID { get => id; set => SetProperty(ref id, value); }
        public int NumCode { get => numCode; set => SetProperty(ref numCode, value); }
        public string CharCode { get => charCode; set => SetProperty(ref charCode, value); }
        public decimal Nominal { get => nominal; set => SetProperty(ref nominal, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public decimal Value { get => valueCurrency; set => SetProperty(ref valueCurrency, value); }
        public decimal Previous { get => previous; set => SetProperty(ref previous, value); }
        #endregion

        #region Методы
        /// <summary>Безпараметрический конструктор</summary>
        public CurrencyVM() { }

        /// <summary>Конструктор с передачей DTO типа</summary>
        /// <param name="other">Экземпляр DTO типа</param>
        public CurrencyVM(CurrencyDto dto) => CopyFromDTO(dto);

        /// <summary>Конструктор с передачей ICurrencyVM</summary>
        /// <param name="other">Экземпляр ICurrencyVM</param>
        public CurrencyVM(ICurrencyVM other) => CopyFrom(other);

        public override CurrencyDto CopyDTO()
            => new CurrencyDto(CurrencyKey, ID, NumCode, CharCode, Nominal, Name, Value, Previous);

        public override void CopyFromDTO(CurrencyDto dto)
        {
            CurrencyKey = dto.CurrencyKey;
            ID = dto.ID;
            NumCode = dto.NumCode;
            CharCode = dto.CharCode;
            Nominal = dto.Nominal;
            Name = dto.Name;
            Value = dto.Value;
            Previous = dto.Previous;            
        }

        public void CopyFrom(ICurrencyVM other)
        {
            CurrencyKey = other.CurrencyKey;
            ID = other.ID;
            NumCode = other.NumCode;
            CharCode = other.CharCode;
            Nominal = other.Nominal;
            Name = other.Name;
            Value = other.Value;
            Previous = other.Previous;
        }
       
        public override bool EqualValues(CurrencyDto other)
            => other.CurrencyKey == CurrencyKey
            && other.ID == ID
            && other.NumCode == NumCode
            && other.CharCode == CharCode
            && other.Nominal == Nominal
            && other.Name == Name
            && other.Value == Value
            && other.Previous == Previous;
        #endregion
    }
}
