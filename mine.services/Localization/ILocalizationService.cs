﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Localization
{
    public interface ILocalizationService
    {
        string GetResource(string format);
    }
}
