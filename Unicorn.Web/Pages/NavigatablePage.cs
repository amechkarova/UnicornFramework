// <copyright file="NavigatablePage.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web.Pages
{
    public abstract class NavigatablePage : BasePage
    {
        public NavigatablePage()
        {
            NavigationService = ServiceContainer.Resolve<IDriver>();
        }

        protected INavigationService NavigationService { get; }

        public abstract string Url { get; }

        public void Open()
        {
            NavigationService.GoToUrl(Url);
            WaitForPageToLoad();
        }

        // implement the Template design pattern
        public abstract void WaitForPageToLoad();
    }
}
