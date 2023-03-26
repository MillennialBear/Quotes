using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common
{
    public class OnPropertyChangedClass : INotifyPropertyChanged
    {
        /// <summary>Событие для извещения об изменения свойства</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Метод для вызова события извещения об изменении свойства</summary>
        /// <param name="prop">Изменившееся свойство или список свойств через разделители "\\/\r \n()\"\'-"</param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            string[] names = prop.Split("\\/\r \n()\"\'-".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (names.Length != 0)
                foreach (string _prp in names)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prp));
        }

        /// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
        /// <param name="propList">Последовательность имён свойств</param>
        public void OnPropertyChanged(IEnumerable<string> propList)
        {
            foreach (string _prp in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prp));
        }

        /// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
        /// <param name="propList">Последовательность свойств</param>
        public void OnPropertyChanged(IEnumerable<PropertyInfo> propList)
        {
            foreach (PropertyInfo _prp in propList)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prp.Name));
        }

        /// <summary>Метод для вызова события извещения об изменении всех свойств</summary>
        public void OnAllPropertyChanged() => OnPropertyChanged(GetType().GetProperties());

        #region Виртуальные защищённые методы для изменения значений свойств
        /// <summary>Виртуальный метод определяющий изменения в значении поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (fieldProperty != null && !fieldProperty.Equals(newValue) || fieldProperty == null && newValue != null)
                PropertyNewValue(ref fieldProperty, newValue, propertyName);
        }

        /// <summary>Виртуальный метод изменяющий значение поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            fieldProperty = newValue;
            OnPropertyChanged(propertyName);
        }
        #endregion
    }
}
