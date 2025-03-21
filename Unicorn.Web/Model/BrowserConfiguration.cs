// <copyright file="BrowserConfiguration.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web.Enums;

namespace Unicorn.Web.Model
{
    public class BrowserConfiguration
    {
        public BrowserConfiguration()
        {
        }

        public BrowserConfiguration(Browser browser, BrowserBehavior browserBehavior)
        {
            Browser = browser;
            BrowserBehavior = browserBehavior;
            ExecutionType = ExecutionType.Regular;
        }

        public Browser Browser { get; set; }

        public BrowserBehavior BrowserBehavior { get; set; }

        public ExecutionType ExecutionType { get; set; }
    }
}
