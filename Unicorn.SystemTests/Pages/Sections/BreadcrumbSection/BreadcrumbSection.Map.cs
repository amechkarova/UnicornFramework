// <copyright file="BreadcrumbSection.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;
using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class BreadcrumbSection : BasePage
    {
        public Anchor BreadCrumb => ElementCreateService.CreateByClass<Anchor>("woocommerce-breadcrumb");
        // public Div BreadCrumb => ElementCreateService.CreateByClass<Div>("woocommerce-breadcrumb");
    }
}
