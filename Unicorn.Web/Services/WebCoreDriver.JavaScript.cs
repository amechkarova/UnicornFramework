// <copyright file="WebCoreDriver.JavaScript.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.Services
{
    public partial class WebCoreDriver : IJavaScriptService
    {
        public object Execute(string script)
        {
            return ((IJavaScriptExecutor)_webDriver).ExecuteScript(script);
        }
    }
}
