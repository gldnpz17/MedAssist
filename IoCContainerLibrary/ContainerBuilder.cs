using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerLibrary
{
    /// <summary>
    /// A builder class for the IoC container.
    /// </summary>
    public class ContainerBuilder
    {
        private IContainer _container = new Container();

        /// <summary>
        /// Registers the types for the IoC container.
        /// </summary>
        /// <param name="source">The type of the returned instance.</param>
        /// <param name="destination">The type of the requested instance.</param>
        /// <param name="isSingleton">Whether or not a singleton is returned.</param>
        public void Register(Type source, Type destination, bool isSingleton)
        {
            _container.RegisterType(source, destination, isSingleton);
        }

        /// <summary>
        /// Returns the IoC container.
        /// </summary>
        /// <returns>The IoC container constrcuted</returns>
        public IContainer Build()
        {
            return _container;
        }
    }
}
