// <copyright file="PurchaseTests.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.SystemTests.Pages;
using Unicorn.Web;
using Unicorn.Web.Enums;
using Unicorn.Web.Plugins.Browser.Attributes;

namespace Unicorn.SystemTests
{
    [TestFixture]
    [ExecutionBrowser(Browser.Chrome, BrowserBehavior.RestartEveryTime)]
    public class PurchaseTests : WebTest
    {
        private static HomePage _homePage;
        private static CartPage _cartPage;
        private static CheckoutPage _checkoutPage;
        private static PurchaseFacade _purchaseFacade;

        public override void ClassInit()
        {
            //ChromeOptions chromeOptions = new ();
            //chromeOptions.PageLoadStrategy = OpenQA.Selenium.PageLoadStrategy.Eager;
            //App.AddBrowserOptions(chromeOptions);
        }

        public override void TestInit()
        {
            _homePage = App.Create<HomePage>();
            _cartPage = App.Create<CartPage>();
            _checkoutPage = App.Create<CheckoutPage>();
            _purchaseFacade = new PurchaseFacade(_homePage, _cartPage, _checkoutPage);
        }

        [Test]
        [ExecutionBrowser(Browser.Firefox, BrowserBehavior.RestartEveryTime)]
        public void PurchaseFalcon9WithoutFacade()
        {
            App.GoTo<HomePage>();
            _homePage.Open();
            _homePage.AddItemToCart("Falcon 9");
            _cartPage.ApplyCoupon("happybirthday");
            _cartPage.AssertMessageNotification("Coupon code applied successfully.");
            _cartPage.UpdateQuantity(3);
            _cartPage.AssertTotal("174.00€");
            _cartPage.ProceedToCheckout();

            var billingInfo = new BillingInfo()
            {
                FirstName = "Aneliya",
                LastName = "Mechkarova",
                CompanyName = "AMechkarova",
                Country = "Bulgaria",
                Address1 = "Second street",
                Address2 = "23",
                Town = "Semchinovo",
                State = "Pazardzhik",
                PostCode = "4452",
                Phone = "0897323846",
                Email = "aneliya.mechkarova@gmail.com",
            };
            _checkoutPage.FillInBillingInfo(billingInfo);
            _checkoutPage.AssertOrderReceived();
        }

        [Test]
        public void PurchaseFalcon9WithFacade()
        {
            var billingInfo = new BillingInfo()
            {
                FirstName = "Aneliya",
                LastName = "Mechkarova",
                CompanyName = "AMechkarova",
                Country = "Bulgaria",
                Address1 = "Second street",
                Address2 = "23",
                Town = "Semchinovo",
                State = "Pazardzhik",
                PostCode = "4452",
                Phone = "0897323846",
                Email = "aneliya.mechkarova@gmail.com",
            };

            _purchaseFacade.PurchaseItem("Falcon 9", "happybirthday", 3, "174.00", billingInfo);
        }
    }
}
