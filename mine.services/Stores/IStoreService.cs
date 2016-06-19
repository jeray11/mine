using mine.core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Stores
{
    public interface IStoreService
    {
        List<Store> GetAllStores();
    }
}
