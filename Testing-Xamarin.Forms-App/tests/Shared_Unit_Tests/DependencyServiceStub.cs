using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wired_Brain_Coffee;

namespace Shared_Unit_Tests
{
    public class DependencyServiceStub : IDependencyService
    {
        private Dictionary<Type, object> registeredServices = new Dictionary<Type, object>();

        public T Get<T>() where T : class
        {
            return (T)registeredServices[typeof(T)];
        }

        public void Register<T>(object impl)
        {
            this.registeredServices[typeof(T)] = impl;
        }
    }
}
