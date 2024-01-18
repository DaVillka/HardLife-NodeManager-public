using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Reflection
{
    public static List<Type> GetTypesImplementingClass(Type baseType)
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(baseType))
            .ToList();
    }public static List<Type> GetTypesImplementingClass(Type baseType, Type attribute)
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(baseType) && t.GetCustomAttribute(attribute, false) != null)
            .ToList();
    }
    public static List<Type> GetTypesImplementingInterface(Type baseType)
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i == baseType))
            .ToList();
    }
    public static List<T> CreateInstances<T>(List<Type> types, params object[] args)
    {        
        return types
            .Where(t => typeof(T)
            .IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .Select(t => (T)Activator.CreateInstance(t, args))
            .ToList();
    }
}