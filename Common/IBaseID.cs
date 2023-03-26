using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>Базовый интерфейс для типов с ID</summary>
    public interface IBaseID
    {
        /// <summary>Идентификатор</summary>
        string CurrencyKey { get; set; }
    }
}
