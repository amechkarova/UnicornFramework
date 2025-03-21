// <copyright file="CssFindStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.FindStrategies
{
    public class CssFindStrategy : FindStrategy
    {
        public CssFindStrategy(string value)
            : base(value)
        {
        }

        public override By Convert() => By.CssSelector(Value);
    }
}
