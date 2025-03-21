// <copyright file="MenuSection.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class MenuSection
    {
        public void OpenHomePage()
        {
            HomeLink.Click();
        }

        public void OpenBlogPage()
        {
            BlogLink.Click();
        }

        public void OpenCartPage()
        {
            CartLink.Click();
        }

        public void OpenCheckoutPage()
        {
            CheckoutLink.Click();
        }

        public void OpenContactFormPage()
        {
            ContactFormLink.Click();
        }

        public void OpenMyAccountPage()
        {
            MyAccountLink.Click();
        }

        public void OpenPromotionsPage()
        {
            PromotionsLink.Click();
        }
    }
}
