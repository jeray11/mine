using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Infrastructure
{
    public class ContainerManager
    {
        private readonly IContainer _container;
        public ContainerManager(IContainer container)
        {
            this._container = container;
        }
        public virtual IContainer Container
        {
            get
            {
                return _container;
            }
        }

        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }
    }
}
