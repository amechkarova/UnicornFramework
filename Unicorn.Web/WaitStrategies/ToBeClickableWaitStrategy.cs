// <copyright file="ToBeClickableWaitStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.WaitStrategies
{
    public class ToBeClickableWaitStrategy : WaitStrategy
    {
        public ToBeClickableWaitStrategy(int? timeoutIntervalSeconds = null, int? sleepIntervalSeconds = null)
            : base(timeoutIntervalSeconds, sleepIntervalSeconds)
        {
            int timeoutSeconds = timeoutIntervalSeconds ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ElementToBeClickableTimeout;
            TimeoutInterval = TimeSpan.FromSeconds(timeoutSeconds);
        }

        public override void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by)
        {
            WaitUntil(s => IsElementClickable(searchContext, by), driver);
        }

        private bool IsElementClickable(ISearchContext searchContext, By by)
        {
            var element = searchContext.FindElement(by);
            return element != null && element.Enabled;
        }
    }
}
