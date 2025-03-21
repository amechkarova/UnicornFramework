// <copyright file="InnerTextContainingFindStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.FindStrategies
{
    public class InnerTextContainingFindStrategy : FindStrategy
    {
        public InnerTextContainingFindStrategy(string value)
            : base(value)
        {
        }

        public override By Convert() => By.XPath($"//*[contains(text(), '{Value}')]");
    }
}
