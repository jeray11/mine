using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Common
{
    public class CurrencySelectorModel
    {
        public CurrencySelectorModel()
        {
            AvailableCurrencies = new List<CurrencyModel>();
        }

        public IList<CurrencyModel> AvailableCurrencies { get; set; }

        public int CurrentCurrencyId { get; set; }
    }
}