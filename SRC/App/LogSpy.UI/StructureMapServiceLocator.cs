using System;
using System.Collections.Generic;
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
}