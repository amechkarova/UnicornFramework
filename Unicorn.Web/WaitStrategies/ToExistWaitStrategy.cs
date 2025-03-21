// <copyright file="ToExistWaitStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.WaitStrategies
{
    public class ToExistWaitStrategy : WaitStrategy
    {
        public ToExistWaitStrategy(int? timeoutIntervalSeconds = null, int? sleepIntervalSeconds = null)
            : base(timeoutIntervalSeconds, sleepIntervalSeconds)
        {
            int timeoutSeconds = timeoutIntervalSeconds ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ElementToExistTimeout;
            TimeoutInterval = TimeSpan.FromSeconds(timeoutSeconds);
        }

        public override void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by)
        {
           WaitUntil(s => DoesElementExists(searchContext, by), driver);
        }

        private bool DoesElementExists(ISearchContext searchContext, By by)
        {
            return searchContext.FindElement(by) != null;
        }
    }
}
