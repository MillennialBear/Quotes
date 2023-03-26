using System;
using System.Runtime.Serialization;

namespace QuotesModelBase
{
    /// <summary>Тип для представления ошибок</summary>
    [Serializable]
    public class QuotesModelException : Exception
    {
        /// <summary>Свойство со значением ошибки</summary>
        public QuotesModelExceptionEnum ValueException { get; } = QuotesModelExceptionEnum.Default;

        #region Конструкторы
        public QuotesModelException(string message)
            : base(message) { }
        public QuotesModelException(QuotesModelExceptionEnum valueException)
            => ValueException = valueException;
        public QuotesModelException(string message, QuotesModelExceptionEnum valueException)
            : base(message)
            => ValueException = valueException;
        public QuotesModelException(string message, Exception innerException)
            : base(message, innerException) { }
        public QuotesModelException(QuotesModelExceptionEnum valueException, Exception innerException)
            : base(null, innerException)
            => ValueException = valueException;
        public QuotesModelException(string message, QuotesModelExceptionEnum valueException, Exception innerException)
            : base(message, innerException)
            => ValueException = valueException;

        public QuotesModelException()
        {
        }

        protected QuotesModelException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        public override string ToString() => ValueException + Environment.NewLine + base.ToString();
    }
}
