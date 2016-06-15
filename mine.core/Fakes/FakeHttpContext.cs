using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace mine.core.Fakes
{
    public class FakeHttpContext : HttpContextBase
    {
        public FakeHttpContext(string relativeUrl) { }
    }
}
