// <copyright file="WaitStrategy.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Unicorn.Web
{
    public abstract class WaitStrategy
    {
        private const int DEFAULT_TIMEOUT = 30;

        protected WaitStrategy(int? timeoutIntervalSeconds = null, int? sleepIntervalSeconds = null)
        {
            TimeoutInterval = TimeSpan.FromSeconds(timeoutIntervalSeconds ?? DEFAULT_TIMEOUT);
            SleepInterval = TimeSpan.FromSeconds(sleepIntervalSeconds ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval);
        }

        protected TimeSpan TimeoutInterval { get; set; }

        protected TimeSpan SleepInterval { get; set; }

        public abstract void WaitUntil(ISearchContext searchContext, IWebDriver driver, By by);

        protected void WaitUntil(Func<ISearchContext, bool> waitCondition, IWebDriver driver)
        {
            var webDriverWait = new WebDriverWait(new SystemClock(), driver, TimeoutInterval, SleepInterval);
            webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            webDriverWait.Until(waitCondition);
        }
    }
}
