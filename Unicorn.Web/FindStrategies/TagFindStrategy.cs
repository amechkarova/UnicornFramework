// <copyright file="TagFindStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.FindStrategies
{
    public class TagFindStrategy : FindStrategy
    {
        public TagFindStrategy(string value)
            : base(value)
        {
        }

        public override By Convert() => By.TagName(Value);
    }
}
