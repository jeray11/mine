using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.test.console
{
    class Program
    {
        static void Main(string[] args)
        {
          var converter=  TypeDescriptor.GetConverter(typeof(CustomerType));
         var obj= converter.ConvertFromInvariantString("1");
         CustomerType type = (CustomerType)obj;
        }
    }

    public enum CustomerType 
    {
        Guest=1,
        Student=2
    }
}
