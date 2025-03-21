// <copyright file="INavigationService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public interface INavigationService
    {
        void GoToUrl(string url);

        Uri Url { get; }
    }
}
