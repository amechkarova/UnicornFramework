// <copyright file="SearchSection.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class SearchSection : BasePage
    {
        public void Search(string searchString)
        {
            SearchField.TypeText(searchString);
            SearchButton.Click();
        }
    }
}
