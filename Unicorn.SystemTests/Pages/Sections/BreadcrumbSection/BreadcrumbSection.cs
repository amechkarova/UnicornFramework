// <copyright file="BreadcrumbSection.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;
using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class BreadcrumbSection : BasePage
    {
        public void OpenBreadcrumbItem(string item)
        {
            BreadCrumb.CreateByLinkText<Anchor>(item).Click();
        }
    }   
}
