// <copyright file="MenuSection.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;
using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class MenuSection : BasePage
    {
        public Anchor HomeLink => ElementCreateService.CreateByLinkText<Anchor>("Home");

        public Anchor BlogLink => ElementCreateService.CreateByLinkText<Anchor>("Blog");

        public Anchor CartLink => ElementCreateService.CreateByLinkText<Anchor>("Cart");

        public Anchor CheckoutLink => ElementCreateService.CreateByLinkText<Anchor>("Checkout");

        public Anchor ContactFormLink => ElementCreateService.CreateByLinkText<Anchor>("Contact Form");

        public Anchor MyAccountLink => ElementCreateService.CreateByLinkText<Anchor>("My Account");

        public Anchor PromotionsLink => ElementCreateService.CreateByLinkText<Anchor>("Promotions");

    }
}
