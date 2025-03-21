// <copyright file="CheckoutPage.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.SystemTests.Pages.Sections;

namespace Unicorn.SystemTests.Pages
{
    public partial class CheckoutPage : NavigatableShopPage
    {
        public override string Url => "https://demos.bellatrix.solutions/cart/";

        public override void WaitForPageToLoad()
        {
            FirstName.ToExists().WaitToBe();
        }

        public BreadcrumbSection Breadcrumb => new BreadcrumbSection();

        public void FillInBillingInfo(BillingInfo billingInfo)
        {
            FirstName.TypeText(billingInfo.FirstName);
            LastName.TypeText(billingInfo.LastName);
            CompanyName.TypeText(billingInfo.CompanyName);
            GetCountryOptionByName(billingInfo.Country).Click();
            StreetAddress.TypeText(billingInfo.Address1);
            ApartmentInfo.TypeText(billingInfo.Address2);
            Town.TypeText(billingInfo.Town);
            GetStateOptionByName(billingInfo.State).Click();
            Postcode.TypeText(billingInfo.PostCode);
            Phone.TypeText(billingInfo.Phone);
            Email.TypeText(billingInfo.Email);

            if (billingInfo.ShouldCreateAccount)
            {
                CreateAccountCheckbox.Check();
            }

            if (billingInfo.CheckPayment)
            {
                CheckPaymentsRadiobutton.Click();
            }

            BrowserService.WaitForAjax();
            PlaceOrderBtn.Click();
            BrowserService.WaitForAjax();
        }
    }
}
