// <copyright file="SearchSection.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;
using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class SearchSection : BasePage
    {
        public TextField SearchField => ElementCreateService.CreateById<TextField>("woocommerce-product-search-field-0");

        public Button SearchButton => ElementCreateService.CreateByLinkText<Button>("Search");
    }
}
