using mine.core.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Topics
{
    public interface ITopicService
    {
        Topic GetTopicBySystemName(string systemName, int storeId = 0);
    }
}
