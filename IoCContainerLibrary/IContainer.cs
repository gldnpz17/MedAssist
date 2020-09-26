using System;

namespace IoCContainerLibrary
{
    //A simple self-made IoC container(in case third party libraries aren't allowed).

    /// <summary>
    /// A simple IoC container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers the type pairings for the container.
        /// </summary>
        /// <param name="source">The type of the returned instance.</param>
        /// <param name="target">The type of the instance requested.</param>
        /// <param name="isSingleton">Whether or not a singleton is returned.</param>
        void RegisterType(Type source, Type target, bool isSingleton);

        /// <summary>
        /// Gets an instance of type T.
        /// </summary>
        /// <typeparam name="T">The requested instance's type.</typeparam>
        /// <returns>An instance of type T</returns>
        T GetInstance<T>();
    }
}