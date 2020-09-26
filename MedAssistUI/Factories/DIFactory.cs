using IoCContainerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistUI.Factories
{
    public class DIFactory<T> : IDIFactory<T>
    {
        private IContainer _container;

        public DIFactory(IContainer container)
        {
            _container = container;
        }

        public T GetInstance()
        {
            return _container.GetInstance<T>();
        }
    }
}
