// <copyright file="LinkTextFindStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.FindStrategies
{
    public class LinkTextFindStrategy : FindStrategy
    {
        public LinkTextFindStrategy(string value)
            : base(value)
        {
        }

        public override By Convert() => By.LinkText(Value);
    }
}
