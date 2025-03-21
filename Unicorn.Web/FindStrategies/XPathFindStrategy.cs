// <copyright file="XPathFindStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.FindStrategies
{
    public class XPathFindStrategy : FindStrategy
    {
        public XPathFindStrategy(string value)
            : base(value)
        {
        }

        public override By Convert() => By.XPath(Value);
    }
}
