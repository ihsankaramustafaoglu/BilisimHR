using System;
using System.Collections.Generic;

namespace bilisimHR.Common.Helper.ServiceLocator
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> registeredServices = new Dictionary<Type, object>();

        public static T GetService<T>()
        {
            return (T)registeredServices[typeof(T)];
        }

        public static void RegisterService<T>(T service)
        {
            registeredServices[typeof(T)] = service;
        }

        public static int Count
        {
            get { return registeredServices.Count; }
        }
    }
}
