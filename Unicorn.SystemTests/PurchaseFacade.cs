// <copyright file="PurchaseFacade.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.SystemTests.Pages;

namespace Unicorn.SystemTests
{
    public class PurchaseFacade
    {
        private readonly HomePage _homePage;
        private readonly CartPage _cartPage;
        private readonly CheckoutPage _checkoutPage;

        public PurchaseFacade(HomePage homePage, CartPage cartPage, CheckoutPage checkoutPage)
        {
            _homePage = homePage;
            _cartPage = cartPage;
            _checkoutPage = checkoutPage;
        }

        public void PurchaseItem(string rocketName, string coupon, int quantity, string expectedPrice, BillingInfo billingInfo)
        {
            _homePage.Open();
            _homePage.AddItemToCart(rocketName);
            _cartPage.ApplyCoupon(coupon);
            _cartPage.AssertMessageNotification("Coupon code applied successfully");
            _cartPage.UpdateQuantity(quantity);
            _cartPage.AssertTotal(expectedPrice);
            _cartPage.ProceedToCheckout();
            _checkoutPage.FillInBillingInfo(billingInfo);
            _checkoutPage.AssertOrderReceived();
        }

    }
}
