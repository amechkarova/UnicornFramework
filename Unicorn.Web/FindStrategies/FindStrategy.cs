﻿// <copyright file="FindStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web.FindStrategies
{
    public abstract class FindStrategy
    {
        protected FindStrategy(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public abstract OpenQA.Selenium.By Convert();
    }
}
