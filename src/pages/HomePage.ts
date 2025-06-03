import { Page, Locator, expect } from '@playwright/test';
import { NavigatableShopPage } from './NavigatableShopPage';

export class HomePage extends NavigatableShopPage {
  // Define the URL for the home page
  public readonly url: string = '/'; // Assuming baseURL is set in playwright.config.ts or fixture

  // Locators corresponding to HomePage.Map.cs
  public get addToCartButtonFalcon9(): Locator {
    return this.page.locator("[data-product_id='28']");
  }

  public get addToCartButtonSaturnV(): Locator {
    return this.page.locator("[data-product_id='29']");
  }

  public get viewCartButton(): Locator {
    return this.page.locator(".added_to_cart.wc-forward");
  }

  public get messageAlert(): Locator {
    return this.page.locator(".woocommerce-message");
  }

  // Method to get product box by name, similar to GetProductBoxByName(name)
  public getProductBoxByName(name: string): Locator {
    // Using XPath to find the h2 with the product name and then get its parent <a> tag
    return this.page.locator(`//h2[@class='woocommerce-loop-product__title' and text()='${name}']/ancestor::li[contains(@class, 'product')]//a[1]`);
  }

  public getProductTitleByName(name: string): Locator {
    return this.page.locator(`//h2[@class='woocommerce-loop-product__title' and text()='${name}']`);
  }

  // Method to get "Add to cart" button by product name
  public getAddToCartButtonByName(name: string): Locator {
    // This locator assumes that the product name is identifiable in the ancestor 'li' element
    // and then finds the 'Add to cart' button within that product's container.
    // This might need adjustment based on actual HTML structure.
    // Example: find the product 'li' by the 'h2' text, then find the button within it.
    return this.page.locator(`//h2[@class='woocommerce-loop-product__title' and text()='${name}']/ancestor::li[contains(@class, 'product')]//a[contains(@class, 'add_to_cart_button')]`);
  }


  constructor(page: Page) {
    super(page);
  }

  // Implement waitForPageToLoad for HomePage
  async waitForPageToLoad(): Promise<void> {
    // Wait for a unique and stable element of the home page to be visible.
    // For example, wait for the Falcon 9 "Add to cart" button or a specific product title.
    // This is equivalent to `AddToCartButtonFalcon9.ToExists().WaitToBe();` or waiting for a main element.
    await expect(this.getProductTitleByName('Falcon 9')).toBeVisible({ timeout: 10000 });
    await expect(this.addToCartButtonFalcon9).toBeVisible({ timeout: 10000 });
  }

  // Page Actions corresponding to HomePage.cs

  /**
   * Adds a specified item (rocket) to the cart.
   * @param rocketName The name of the rocket to add to the cart (e.g., "Falcon 9").
   */
  async addItemToCart(rocketName: string): Promise<void> {
    const addToCartButton = this.getAddToCartButtonByName(rocketName);
    await addToCartButton.click();
    // In Playwright, auto-waits usually handle AJAX, but if specific network idle is needed:
    await this.page.waitForLoadState('networkidle', { timeout: 5000 }); // wait for network to be idle
    // Or, wait for the "View cart" button to become visible as a confirmation
    await expect(this.viewCartButton).toBeVisible();
  }

  async openCartFromNotification(): Promise<void> {
    await this.viewCartButton.click();
    // Add appropriate waitForPageToLoad for the cart page if navigating,
    // or wait for a specific element on the cart page.
    // For now, assume navigation is handled by a CartPage object later.
  }

  // Assertion Methods corresponding to HomePage.Assert.cs

  /**
   * Asserts that the product box link for a given product name matches the expected link.
   * @param name The name of the product.
   * @param expectedLink The expected partial or full URL.
   */
  async assertProductBoxLink(name: string, expectedLink: string): Promise<void> {
    const productLink = this.getProductBoxByName(name).locator('a').first(); // Assuming the link is the first 'a' in the box
    await expect(productLink).toHaveAttribute('href', new RegExp(expectedLink)); // Use RegExp for partial match if needed
  }

  /**
   * Asserts that the "View cart" button is visible after adding an item.
   */
  async assertViewCartButtonVisible(): Promise<void> {
    await expect(this.viewCartButton).toBeVisible();
  }

  /**
   * Asserts that the correct message is displayed after adding an item to the cart.
   * @param expectedMessage The expected message or a part of it.
   */
  async assertMessageAlertText(expectedMessage: string | RegExp): Promise<void> {
    await expect(this.messageAlert).toContainText(expectedMessage);
  }
}
