﻿using mine.core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core
{
   public interface IStoreContext
    {
       Store CurrentStore { get; }
    }
}
