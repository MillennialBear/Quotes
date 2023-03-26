using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>Базовый класс для DTO типов с уникальным и неизменяемым ID</summary>
    public class BaseIdDTO : IEquatable<BaseIdDTO>, IEqualityComparer<BaseIdDTO>
    {
        /// <summary>Идентификатор экземпляра</summary>
        public string CurrencyKey { get; }

        /// <summary>Конструктор задающий ID</summary>
        /// <param name="currencyKey">ID экземпляра</param>
        public BaseIdDTO(string currencyKey) => CurrencyKey = currencyKey;

        #region Методы реализующие интерфейсы 
        public bool Equals(BaseIdDTO other) => CurrencyKey == other.CurrencyKey;
        public override bool Equals(object obj) => obj is BaseIdDTO other && Equals(other);

        public bool Equals(BaseIdDTO x, BaseIdDTO y) => x.Equals(y);

        public override int GetHashCode() => CurrencyKey.GetHashCode();

        public int GetHashCode(BaseIdDTO obj) => obj.GetHashCode();
        #endregion
    }
}
