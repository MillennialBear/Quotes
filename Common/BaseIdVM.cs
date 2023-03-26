namespace Common
{
    /// <summary>Базовый класс для типов VM с ID и обменивающийся данными с DTO типом</summary>
    /// <typeparam name="TDto">Производный от BaseIdDTO тип</typeparam>
    public abstract class BaseIdVM<TDto> : OnPropertyChangedClass, IEquatableValues<TDto>, IBaseID
        where TDto : BaseIdDTO
    {
        /// <summary>Приватное поля для хранения значения свойства</summary>
        private string currencyKey;

        public string CurrencyKey { get => currencyKey; set => SetProperty(ref currencyKey, value); }

        /// <summary>Создание экземпляра DTO типа со скопированными значениями</summary>
        /// <returns>Новый экземпляр DTO типа</returns>
        public abstract TDto CopyDTO();

        /// <summary>Копирование значений из DTO типа</summary>
        /// <param name="dto">Экземпляр DTO типа</param>
        public abstract void CopyFromDTO(TDto dto);

        public abstract bool EqualValues(TDto other);

        /// <summary>Безпараметрический конструктор</summary>
        protected BaseIdVM() { }
        /// <summary>Конструктор с передачей ID</summary>
        /// <param name="id">Значение ID</param>
        protected BaseIdVM(string id) => CurrencyKey = id;
        /// <summary>Конструктор с передачей DTO типа</summary>
        /// <param name="dto">Экземпляр DTO типа</param>
        protected BaseIdVM(TDto dto) => CurrencyKey = dto.CurrencyKey;
    }
}
