using Autofac;
using Autofac.Integration.Mvc;
using mine.core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.web.framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
        }
    }
}
