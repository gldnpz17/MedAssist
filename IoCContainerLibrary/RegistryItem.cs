using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerLibrary
{
    internal class RegistryItem
    {
        internal Type Source { get; }
        internal Type Destination { get; }
        internal bool IsSingleton { get; }

        internal RegistryItem(Type source, Type destination, bool isSingleton)
        {
            Source = source;
            Destination = destination;
            IsSingleton = isSingleton;
        }
    }
}
