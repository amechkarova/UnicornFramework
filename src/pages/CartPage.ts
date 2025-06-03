import { Page, Locator, expect } from '@playwright/test';
import { NavigatableShopPage } from './NavigatableShopPage';

export class CartPage extends NavigatableShopPage {
  // Define the URL for the cart page
  public readonly url: string = '/cart/'; // Relative to the base URL

  // Locators corresponding to CartPage.Map.cs
  public get couponCodeTextField(): Locator {
    return this.page.locator("#coupon_code");
  }

  public get applyCouponButton(): Locator {
    return this.page.locator("button[name='apply_coupon']"); // More specific: button with name
  }

  // Assuming there's only one primary quantity box or the first one is targeted.
  // If multiple items, this would need to be more specific, e.g., by product.
  public get quantityBox(): Locator {
    // This locator targets the quantity input for the first cart item.
    // It might need to be parameterized if dealing with multiple items.
    return this.page.locator(".cart_item .quantity input[type='number']").first();
  }

  public get updateCartButton(): Locator {
    return this.page.locator("button[name='update_cart']"); // More specific: button with name
  }

  public get messageAlert(): Locator {
    // This could be a general message area, or specific to coupon success/failure.
    // For coupon success, it's often '.woocommerce-message'.
    return this.page.locator(".woocommerce-message");
  }

  public get errorMessageAlert(): Locator {
    // For errors, it's often '.woocommerce-error'.
    return this.page.locator(".woocommerce-error");
  }

  public get totalSpan(): Locator {
    // Targets the total price in the cart totals section.
    return this.page.locator(".cart-totals .order-total .woocommerce-Price-amount");
  }

  public get subtotalSpan(): Locator {
    // Targets the subtotal price in the cart totals section.
    return this.page.locator(".cart-totals .cart-subtotal .woocommerce-Price-amount");
  }

  public get proceedToCheckoutButton(): Locator {
    return this.page.locator(".checkout-button");
  }

  // Method to get the displayed price for a specific product in the cart
  public getProductPriceByName(name: string): Locator {
    return this.page.locator(`//td[@class='product-name']/a[text()='${name}']/../following-sibling::td[@class='product-price']//span[@class='woocommerce-Price-amount']`);
  }

  // Method to get the quantity input for a specific product
  public getProductQuantityInputByName(name: string): Locator {
    return this.page.locator(`//td[@class='product-name']/a[text()='${name}']/../following-sibling::td[@class='product-quantity']//input[contains(@id, 'quantity_')]`);
  }

  // Method to get the remove button for a specific product
  public getProductRemoveButtonByName(name: string): Locator {
    return this.page.locator(`//td[@class='product-name']/a[text()='${name}']/../following-sibling::td[@class='product-remove']/a`);
  }


  constructor(page: Page) {
    super(page);
  }

  // Implement waitForPageToLoad for CartPage
  async waitForPageToLoad(): Promise<void> {
    // Wait for a unique and stable element of the cart page to be visible.
    // For example, the "Proceed to checkout" button or the cart totals section.
    await expect(this.page.locator("h1:has-text('Cart')")).toBeVisible({ timeout: 10000 });
    await expect(this.proceedToCheckoutButton).toBeVisible({ timeout: 10000 });
  }

  // Page Actions corresponding to CartPage.cs

  async applyCoupon(couponCode: string): Promise<void> {
    await this.couponCodeTextField.fill(couponCode);
    await this.applyCouponButton.click();
    // Wait for the message indicating coupon application status or for network activity to settle.
    await this.page.waitForLoadState('networkidle', { timeout: 5000 });
    // Or, more robustly, wait for either a success or error message to appear.
    await expect(this.page.locator('.woocommerce-message, .woocommerce-error').first()).toBeVisible();
  }

  async updateQuantityForProduct(productName: string, newQuantity: number | string): Promise<void> {
    const quantityInput = this.getProductQuantityInputByName(productName);
    await quantityInput.fill(newQuantity.toString());
    await this.updateCartButton.click();
    // Wait for the cart to update. This could be waiting for network idle or for a price change.
    await this.page.waitForLoadState('networkidle', { timeout: 7000 });
    // Add an assertion or wait for a specific element that indicates the update is complete.
    await expect(this.updateCartButton).toBeEnabled(); // Wait for button to be usable again
  }

  async removeProduct(productName: string): Promise<void> {
    await this.getProductRemoveButtonByName(productName).click();
    await this.page.waitForLoadState('networkidle', {timeout: 5000});
    // Add an assertion for item removal, e.g. item no longer visible or cart empty message
  }

  async proceedToCheckout(): Promise<void> {
    await this.proceedToCheckoutButton.click();
    // Typically, this would navigate to a CheckoutPage.
    // The CheckoutPage's waitForPageToLoad would handle synchronization.
    // For now, just ensure the click happens and potentially wait for navigation.
    await this.page.waitForNavigation({waitUntil: 'domcontentloaded', timeout: 10000});
  }

  // Assertion Methods corresponding to CartPage.Assert.cs

  async assertCartPageTitle(expectedTitle: string | RegExp = /Cart.*/i): Promise<void> {
    await expect(this.page).toHaveTitle(expectedTitle);
  }

  async assertCouponAppliedSuccessfully(expectedMessage: string | RegExp = /Coupon code applied successfully./i): Promise<void> {
    await expect(this.messageAlert.first()).toContainText(expectedMessage);
  }

  async assertCouponFailed(expectedMessage: string | RegExp = /.*/i): Promise<void> { // Provide a more specific default if known
    await expect(this.errorMessageAlert.first()).toContainText(expectedMessage);
  }

  async assertTotalPrice(expectedPrice: string | RegExp): Promise<void> {
    // Ensure to match the format as displayed on the page, e.g., including currency symbols.
    await expect(this.totalSpan).toHaveText(expectedPrice);
  }

  async assertProductPrice(productName: string, expectedPrice: string | RegExp): Promise<void> {
    const productPriceLocator = this.getProductPriceByName(productName);
    await expect(productPriceLocator).toBeVisible();
    await expect(productPriceLocator).toHaveText(expectedPrice);
  }

  async assertCartIsEmpty(expectedMessage: string | RegExp = /Your cart is currently empty./i): Promise<void> {
    await expect(this.page.locator('.cart-empty.woocommerce-info')).toContainText(expectedMessage);
  }
}
