using System;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Common.Extensions
{
    public class AddServiceAttribute : Attribute
    {
        public Type InterfaceType { get; }
        public ServiceLifetime LifeTime { get; }

        public AddServiceAttribute(Type interfaceType, ServiceLifetime lifeTime)
        {
            InterfaceType = interfaceType;
            LifeTime = lifeTime;
        }
    }
}
