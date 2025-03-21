// <copyright file="ViewCartSection.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;
using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class ViewCartSection : BasePage
    {
        public Anchor ViewCartLink => ElementCreateService.CreateByClass<Anchor>("cart-contents");

        public Span CartAmount => ElementCreateService.CreateByClass<Span>("amount");
    }
}
