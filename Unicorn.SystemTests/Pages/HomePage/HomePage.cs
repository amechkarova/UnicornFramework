// <copyright file="HomePage.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.SystemTests.Pages
{
    public partial class HomePage : NavigatableShopPage
    {
        public override string Url => "https://demos.bellatrix.solutions/";

        public override void WaitForPageToLoad()
        {
            AddToCartButtonFalcon9.ToExists().WaitToBe();
        }

        public void AddItemToCart(string rocketName)
        {
            Open();

            // GetProductBoxByName(rocketName).Click();
            GetProductAddToCartButtonByName(rocketName).Click();
            BrowserService.WaitForAjax();

            // AddToCartButton.Click();
            // MessageAlert.ToExists().WaitToBe();
            ViewCartButton.Click();
        }
    }
}
