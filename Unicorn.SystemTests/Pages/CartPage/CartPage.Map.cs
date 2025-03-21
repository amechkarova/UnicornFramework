// <copyright file="CartPage.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;

namespace Unicorn.SystemTests.Pages
{
    public partial class CartPage
    {
        public TextField QuantityField => ElementCreateService.CreateByCss<TextField>("[class*='input-text qty text']");

        public TextField CouponCodeField => ElementCreateService.CreateById<TextField>("coupon_code");

        public Button ApplyCouponBtn => ElementCreateService.CreateByCss<Button>("[value*='Apply coupon']");

        public Button UpdateCartBtn => ElementCreateService.CreateByCss<Button>("[value*='Update cart']");

        public Button ProceedToCheckoutBtn => ElementCreateService.CreateByXPath<Button>("//a[contains(@href, 'checkout')]");

        public Div MessageAlert => ElementCreateService.CreateByCss<Div>("[class*='woocommerce-message']");

        public Span CartTotal => ElementCreateService.CreateByXPath<Span>("//*[@class='order-total']//span");
    }
}
