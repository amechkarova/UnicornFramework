// <copyright file="WebCoreDriver.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Unicorn.Web.Services
{
    public partial class WebCoreDriver : IDriver
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;

        public WebCoreDriver(IWebDriver wrappedDriver)
        {
            _webDriver = wrappedDriver;
            var timeoutSettings = ConfigurationService.GetSection<WebSettings >().TimeoutSettings;
            _webDriverWait = new WebDriverWait(wrappedDriver, TimeSpan.FromSeconds(timeoutSettings.WaitForAjaxTimeout));
        }
    }
}
