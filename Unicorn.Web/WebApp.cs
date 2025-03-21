// <copyright file="WebApp.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.App;
using Unicorn.Web.Pages;

namespace Unicorn.Web
{
    public class WebApp : BaseApp
    {
        private IDriver _driver;

        public WebApp()
        {
            _driver = ServiceContainer.Resolve<IDriver>();
        }

        public IElementCreateService ElementCreateService => _driver;

        public IBrowserService BrowserService => _driver;

        public INavigationService NavigationService => _driver;

        public ICookiesService CookiesService => _driver;

        public IDialogService DialogService => _driver;

        public IJavaScriptService JavaScriptService => _driver;

        public IInteractionsService InteractionsService => _driver;

        public void AddBrowserOptions<TOption>(TOption cusomtOptions)
            where TOption : class
        {
            // TODO: Change guid with full test class name.
            ServiceContainer.RegisterInstance(cusomtOptions, Guid.NewGuid().ToString());
        }

        public TPage Create<TPage>()
            where TPage : BasePage
        {
            return ServiceContainer.Resolve<TPage>();
        }

        public TPage GoTo<TPage>()
            where TPage : NavigatablePage
        {
            var page = ServiceContainer.Resolve<TPage>();
            page.Open();
            return page;
        }
    }
}
