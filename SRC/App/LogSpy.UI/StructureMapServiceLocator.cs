using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace LogSpy.UI
{
    public class StructureMapServiceLocator: ServiceLocatorImplBase
    {
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return ObjectFactory.GetInstance(serviceType);
            }
            return ObjectFactory.GetNamedInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (IEnumerable<object>) ObjectFactory.GetAllInstances(serviceType) ;
        }

    }

    public static class StructureMapExtensions
    {

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This used for type retrieval")]
        public static T GetInstanceWith<T, TArgument>(this IServiceLocator serviceLocator, TArgument argument)
        {
            if(serviceLocator is StructureMapServiceLocator)
            {
                return ObjectFactory.With<TArgument>(argument).GetInstance<T>();
            }
            throw new InvalidOperationException(Resources.Exceptions.IncompatibleServiceLocator);
        }
    }
}