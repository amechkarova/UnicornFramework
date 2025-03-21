// <copyright file="HomePage.Map.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;

namespace Unicorn.SystemTests.Pages
{
    public partial class HomePage
    {
        public Anchor AddToCartButtonFalcon9 => ElementCreateService.CreateByCss<Anchor>("[data-product_id='28']");

        public Anchor ViewCartButton => ElementCreateService.CreateByCss<Anchor>("[class='added_to_cart wc-forward']");

        public Div MessageAlert => ElementCreateService.CreateByClass<Div>("woocommerce-message");

        public Button AddToCartButton => ElementCreateService.CreateByXPath<Button>("//div[@id='product-28']//button");

        public Anchor GetProductBoxByName(string name)
        {
            return ElementCreateService.CreateByXPath<Anchor>($"//h2[text()='{name}']/parent::a[1]");
        }

        public Anchor GetProductAddToCartButtonByName(string name)
        {
            return ElementCreateService.CreateByXPath<Anchor>($"//h2[text()='{name}']/following::a[1]");
        }
    }
}
