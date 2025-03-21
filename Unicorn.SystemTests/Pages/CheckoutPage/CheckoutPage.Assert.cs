// <copyright file="CheckoutPage.Assert.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.SystemTests.Pages
{
    public partial class CheckoutPage
    {
        public void AssertOrderReceived()
        {
            Assert.That(ReceivedMessage.InnerText, Is.EqualTo("Order received"));
        }
    }
}
