﻿using mine.web.framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Common
{
    public class CurrencyModel : BaseMineViewModel
    {
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }
    }
}