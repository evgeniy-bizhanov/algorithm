using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Algorithms
{
    public static class AttributeHelper
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(object instance)
            where TAttribute : Attribute
        {
            TypeInfo instanceTypeInfo = instance.GetType().GetTypeInfo();
            var attributes = Attribute.GetCustomAttributes(instanceTypeInfo, typeof(TAttribute));

            return attributes.Cast<TAttribute>();
        }

        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(object instance)
            where TAttribute : Attribute
        {
            TypeInfo instanceTypeInfo = instance.GetType().GetTypeInfo();

            var bindings = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
            var methods = instanceTypeInfo.GetMethods(bindings).Where(x => x.GetCustomAttribute<TAttribute>(false) != null);

            return methods.Cast<MethodInfo>();
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this MemberInfo info)
            where TAttribute : Attribute
        {
            return info.GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>();
        }
    }
}