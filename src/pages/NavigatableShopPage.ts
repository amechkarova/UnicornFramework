import { Page, Locator, expect } from '@playwright/test';
import { NavigatablePage } from './NavigatablePage';

/**
 * Represents a base class for pages within the shop section that are directly navigable.
 * It can contain locators and methods for elements common across all such shop pages,
 * like main navigation, header, footer, search bar, etc.
 */
export abstract class NavigatableShopPage extends NavigatablePage {
  // --- Common Shop Header Locators ---
  public get siteLogo(): Locator {
    return this.page.locator('header .custom-logo-link'); // Example
  }

  public get mainMenuHomeLink(): Locator {
    return this.page.locator('#primary-menu >> text=Home'); // Example using Playwright text selector
  }

  public get mainMenuShopLink(): Locator {
    return this.page.locator('#primary-menu >> text=Shop'); // Example
  }

  public get mainMenuCartLink(): Locator {
    return this.page.locator('#primary-menu >> text=Cart');
  }

  public get mainMenuCheckoutLink(): Locator {
    return this.page.locator('#primary-menu >> text=Checkout');
  }

  public get searchInput(): Locator {
    return this.page.locator('header #woocommerce-product-search-field-0'); // Example general search
  }

  public get searchButton(): Locator {
    return this.page.locator('header button[value="Search"]'); // Example
  }

  // --- Common Shop Footer Locators ---
  public get footerCopyright(): Locator {
    return this.page.locator('.site-info >> text=/Copyright © .*/i'); // Example
  }

  constructor(page: Page) {
    super(page);
  }

  // --- Common Shop Actions ---

  async searchForProduct(searchTerm: string): Promise<void> {
    await this.searchInput.fill(searchTerm);
    await this.searchButton.click();
    // Add a wait for search results page to load, typically handled by the target page's waitForPageToLoad
    // For example: await this.page.waitForURL(/**searchResultsPagePattern**/);
  }

  async navigateToCartViaMenu(): Promise<void> {
    await this.mainMenuCartLink.click();
    // The CartPage's waitForPageToLoad will handle synchronization.
    await this.page.waitForURL('**/cart/', {timeout: 5000});
  }

  async navigateToCheckoutViaMenu(): Promise<void> {
    await this.mainMenuCheckoutLink.click();
    // The CheckoutPage's waitForPageToLoad will handle synchronization.
    await this.page.waitForURL('**/checkout/', {timeout: 5000});
  }

  // `url` and `waitForPageToLoad` are abstract in NavigatablePage and will be implemented
  // by concrete pages extending NavigatableShopPage (like a future ShopListingPage, ProductPage, etc.)
}
