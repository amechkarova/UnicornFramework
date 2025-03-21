// <copyright file="CartPage.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.SystemTests.Pages.Sections;

namespace Unicorn.SystemTests.Pages
{
    public partial class CartPage : ShopPage
    {
        public BreadcrumbSection Breadcrumb => new BreadcrumbSection();

        public void ApplyCoupon(string coupon)
        {
            CouponCodeField.TypeText(coupon);
            ApplyCouponBtn.Click();
            BrowserService.WaitForAjax();
        }

        public void UpdateQuantity(int quantity)
        {
            QuantityField.TypeText(quantity.ToString());
            UpdateCartBtn.Click();
            BrowserService.WaitForAjax();
        }

        public void ProceedToCheckout()
        {
            ProceedToCheckoutBtn?.Click();
            BrowserService.WaitUntilPageLoadsCompletely();
        }
    }
}
