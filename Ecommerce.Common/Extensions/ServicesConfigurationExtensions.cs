using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Common.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static IServiceCollection AddServicesFromCurrentDomain(this IServiceCollection services)
        {
            Dictionary<string, MethodInfo> MethodInfoDict = new Dictionary<string, MethodInfo>();

            MethodInfoDict.Add("AddSingleton", GetServiceCollectionMethodInfo("AddSingleton"));
            MethodInfoDict.Add("AddScoped", GetServiceCollectionMethodInfo("AddScoped"));
            MethodInfoDict.Add("AddTransient", GetServiceCollectionMethodInfo("AddTransient"));
            var assss = AppDomain.CurrentDomain.GetAssemblies().ToList().Where(a => a.FullName.Contains("Ecommerce"));
            AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(assembly =>
            {
                assembly.GetTypes().ToList().ForEach(type =>
                {
                    if (Attribute.GetCustomAttribute(type, typeof(AddServiceAttribute)) is AddServiceAttribute attr)
                    {
                        var methodName = "";
                        switch (attr.LifeTime)
                        {
                            case ServiceLifetime.Singleton:
                                methodName = "AddSingleton";
                                break;
                            case ServiceLifetime.Scoped:
                                methodName = "AddScoped";
                                break;
                            case ServiceLifetime.Transient:
                                methodName = "AddTransient";
                                break;
                        }

                        if (string.IsNullOrEmpty(methodName))
                            return;

                        if (!MethodInfoDict.TryGetValue(methodName, out var methodInfo))
                        {
                            throw new Exception($"Cant't find method with name '{methodName}' in IServiceCollection");
                        }

                        if (methodInfo != null)
                        {
                            methodInfo
                                .MakeGenericMethod(attr.InterfaceType, type)
                                .Invoke(services, new object[] {services});
                        }
                    }
                });
            });

            return services;
        }

        private static MethodInfo[] GetExtensionMethods(this Type type)
        {
            var assembleTypes = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = assembly.GetTypes();
                assembleTypes.AddRange(types);
            }

            var query = from t in assembleTypes
                where t.IsSealed && !t.IsGenericType && !t.IsNested
                from method in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false)
                where method.GetParameters()[0].ParameterType == type
                select method;

            return query.ToArray();
        }

        private static MethodInfo GetServiceCollectionMethodInfo(string methodName)
        {
            return typeof(IServiceCollection)
                .GetExtensionMethods()
                .FirstOrDefault(m =>
                    m.IsGenericMethod && m.Name == methodName && m.GetParameters().Length == 1);
        }
    }
}
