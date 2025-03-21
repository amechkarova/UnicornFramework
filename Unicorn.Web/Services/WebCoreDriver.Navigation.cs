// <copyright file="WebCoreDriver.Navigation.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web.Services
{
    public partial class WebCoreDriver : INavigationService
    {
        public Uri Url => new Uri(_webDriver.Url);

        public void GoToUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }
    }
}
