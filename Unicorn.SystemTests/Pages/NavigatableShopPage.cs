// <copyright file="NavigatableShopPage.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.SystemTests.Pages.Sections;
using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages
{
    public abstract class NavigatableShopPage : NavigatablePage
    {
        public MenuSection Menu => new MenuSection();

        public SearchSection SearchItem => new SearchSection();

        public ViewCartSection ViewCart => new ViewCartSection();
    }
}
