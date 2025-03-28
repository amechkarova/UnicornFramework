﻿// <copyright file="BaseApp.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.App
{
    public class BaseApp
    {
        public static T Resolve<T>()
        {
            T result = ServiceContainer.Resolve<T>();
            return result;
        }

        public static void RegisterType<TFrom, TTo>(string name) where TTo : TFrom
        {
             ServiceContainer.RegisterType<TFrom, TTo>(name);
        }

        public static void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            ServiceContainer.RegisterType<TFrom, TTo>();
        }

        public static void RegisterInstance<TFrom>(TFrom instance)
        {
            ServiceContainer.RegisterInstance(instance);
        }
    }
}
