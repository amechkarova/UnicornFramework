// <copyright file="BasePage.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web.Pages
{
    public abstract class BasePage
    {
        public BasePage()
        {
            var driver = ServiceContainer.Resolve<IDriver>();
            ElementCreateService = driver;
            BrowserService = driver;
        }

        protected IElementCreateService ElementCreateService { get; }

        protected IBrowserService BrowserService { get; }
    }
}
