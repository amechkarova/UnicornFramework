// <copyright file="CartPage.Assert.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.SystemTests.Pages
{
    public partial class CartPage
    {
        public void AssertTotal(string expectedTotal)
        {
            BrowserService.WaitForAjax();
            Assert.That(CartTotal.InnerText, Is.EqualTo(expectedTotal));
        }

        public void AssertCartIsUpdated(string expectedMessage)
        {
            BrowserService.WaitForAjax();
            Assert.That(MessageAlert.InnerText, Is.EqualTo(expectedMessage));
        }

        public void AssertMessageNotification(string expectedMessage)
        {
            BrowserService.WaitForAjax();
            Assert.That(MessageAlert.InnerText, Is.EqualTo(expectedMessage));
        }
    }
}
