// <copyright file="ViewCartSection.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web.Pages;

namespace Unicorn.SystemTests.Pages.Sections
{
    public partial class ViewCartSection : BasePage
    {
        public string GetCurrentAmount()
        {
            return CartAmount.InnerText;
        }

        public void OpenCart()
        {
            ViewCartLink.Click();
        }
    }
}
