﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Web.Enums;
using Unicorn.Web.Model;

namespace Unicorn.Web.Plugins.Browser.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class GridAttribute : ExecutionBrowserAttribute
    {
        public GridAttribute(Web.Browser browser, string browserVersion, PlatformType platformType, BrowserBehavior browserBehavior)
            : base(browser, browserBehavior)
        {
            BrowserConfiguration.ExecutionType = ExecutionType.Grid;
            BrowserVersion = browserVersion;
            PlatformType = platformType;
        }

        public string BrowserVersion { get; }
        public PlatformType PlatformType { get; }
    }
}
