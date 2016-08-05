using mine.core.Domain.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Directory
{
    public interface  ICurrencyService
    {
        /// <summary>
        /// Gets all currencies
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Currencies</returns>
        IList<Currency> GetAllCurrencies(bool showHidden = false, int storeId = 0);
        /// <summary>
        /// 根据ID 获取货币信息
        /// </summary>
        /// <param name="customerCurrency"></param>
        /// <returns></returns>
        Currency GetCurrencyById(int customerCurrency);
    }
}
