// <copyright file="CheckoutPage.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;

namespace Unicorn.SystemTests.Pages
{
    public partial class CheckoutPage
    {
        public TextField FirstName => ElementCreateService.CreateById<TextField>("billing_first_name");

        public TextField LastName => ElementCreateService.CreateById<TextField>("billing_last_name");

        public TextField CompanyName => ElementCreateService.CreateById<TextField>("billing_company");

        public Button Country => ElementCreateService.CreateById<Button>("select2-billing_country-container");

        public TextField CountrySearch => ElementCreateService.CreateByXPath<TextField>("//input[contains(@aria-activedescendant, 'country')]");

        public TextField StreetAddress => ElementCreateService.CreateById<TextField>("billing_address_1");

        public TextField ApartmentInfo => ElementCreateService.CreateById<TextField>("billing_address_2");

        public TextField Town => ElementCreateService.CreateById<TextField>("billing_city");

        public Button State => ElementCreateService.CreateById<Button>("select2-billing_state-container");

        public TextField StateSearch => ElementCreateService.CreateByXPath<TextField>("//input[contains(@aria-activedescendant, 'state')]");

        public TextField Postcode => ElementCreateService.CreateById<TextField>("billing_postcode");

        public TextField Phone => ElementCreateService.CreateById<TextField>("billing_phone");

        public TextField Email => ElementCreateService.CreateById<TextField>("billing_email");

        public CheckBox CreateAccountCheckbox => ElementCreateService.CreateById<CheckBox>("createaccount");

        public TextField OrderNotes => ElementCreateService.CreateById<TextField>("order_comments");

        public Button PlaceOrderBtn => ElementCreateService.CreateById<Button>("place_order");

        public RadioButton CheckPaymentsRadiobutton => ElementCreateService.CreateByCss<RadioButton>("[for*='payment_method_cheque']");

        public Span ReceivedMessage => ElementCreateService.CreateByXPath<Span>("//h1");

        public Button GetCountryOptionByName(string countryName)
        {
            return ElementCreateService.CreateByXPath<Button>($"//*[contains(text(), '{countryName}')]");
        }

        public Button GetStateOptionByName(string stateName)
        {
            return ElementCreateService.CreateByXPath<Button>($"//*[contains(text(), '{stateName}')]");
        }
    }
}
