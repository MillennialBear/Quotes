namespace Common
{
    /// <summary>Базовый класс для типов VM с ID и обменивающийся данными с DTO типом</summary>
    /// <typeparam name="TDto">DTO тип</typeparam>
    public abstract class BaseIdVM<TDto> : OnPropertyChangedClass, IEquatableValues<TDto>        
    {
        /// <summary>Создание экземпляра DTO типа со скопированными значениями</summary>
        /// <returns>Новый экземпляр DTO типа</returns>
        public abstract TDto CopyDTO();

        /// <summary>Копирование значений из DTO типа</summary>
        /// <param name="dto">Экземпляр DTO типа</param>
        public abstract void CopyFromDTO(TDto dto);

        public abstract bool EqualValues(TDto other);
    }
}
