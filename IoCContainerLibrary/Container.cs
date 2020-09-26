using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace IoCContainerLibrary
{
    public class Container : IContainer
    {
        private List<RegistryItem> _registry = new List<RegistryItem>();
        private List<object> _singletons = new List<object>();

        public Container()
        {
            _singletons.Add(this);
        }

        public void RegisterType(Type source, Type target, bool isSingleton)
        {
            _registry.Add(new RegistryItem(source, target, isSingleton));
        }

        public T GetInstance<T>()
        {
            //get registry entry
            List<RegistryItem> entries = (from regEntry
                                          in _registry
                                          where regEntry.Destination == typeof(T)
                                          select regEntry).ToList();
            if (entries.Count == 0)//if there are no entries in the registry
            {
                //throw exception
                throw new Exception($"Entry not found for type : {typeof(T)}."); //TO-DO : throw a better exception
            }
            else
            {
                //get the entry
                RegistryItem entry = entries.First();

                if (entry.IsSingleton)//if a singleton is requested
                {
                    //get instance
                    List<object> outputList = (from output
                                               in _singletons
                                               where output.GetType() == entry.Source
                                               select output).ToList();
                    if (outputList.Count == 0)//if there is no singleton instance to return
                    {
                        T output = GetInstanceRecursive(entry);

                        _singletons.Add(output);
                        return output;
                    }
                    else
                    {
                        return (T)outputList.First();
                    }
                }
                else
                {
                    return GetInstanceRecursive(entry);
                }
            }

            T GetInstanceRecursive(RegistryItem registryItem)
            {
                //get constructors
                List<ConstructorInfo> constructorInfos = registryItem.Source.GetConstructors().ToList();
                //select the one with the most parameters
                int maxParamCount = 0;
                ConstructorInfo selectedConstructor = constructorInfos[0];//in case there aren't any, select the default constructor
                foreach (var item in constructorInfos)
                {
                    if (item.GetParameters().ToList().Count > maxParamCount)
                    {
                        maxParamCount = item.GetParameters().ToList().Count;
                        selectedConstructor = item;
                    }
                }
                //get the required arguments
                List<ParameterInfo> parameterInfos = selectedConstructor.GetParameters().ToList();
                //instantiate the arguments
                List<object> args = new List<object>();
                foreach (ParameterInfo item in parameterInfos)
                {
                    Type itemType = item.ParameterType;
                    MethodInfo methodInfo = this.GetType().GetMethod("GetInstance").MakeGenericMethod(itemType);
                    args.Add(methodInfo.Invoke(this, null));
                }

                //instantiate
                return (T)Activator.CreateInstance(registryItem.Source, args.ToArray());
            }
        }
    }
}
