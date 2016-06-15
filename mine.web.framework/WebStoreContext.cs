using mine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Domain.Stores;

namespace mine.web.framework
{
    public class WebStoreContext : IStoreContext
    {
        public Store CurrentStore
        {
            get
            {
                return new Store();
            }
        }
    }
}
