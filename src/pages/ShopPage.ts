import { Page, Locator, expect } from '@playwright/test';
import { NavigatableShopPage } from './NavigatableShopPage';

/**
 * Represents the main Shop page where products are listed.
 */
export class ShopPage extends NavigatableShopPage {
  public readonly url: string = '/shop/'; // URL for the main shop page

  // --- Locators specific to the Shop Page ---
  public get pageTitleHeading(): Locator {
    return this.page.locator("header h1.woocommerce-products-header__title.page-title"); // Example: "Shop" heading
  }

  public get productGrid(): Locator {
    return this.page.locator('ul.products'); // The main container for products
  }

  // Gets all product items currently visible on the page
  public get allProductItems(): Locator {
    return this.productGrid.locator('li.product');
  }

  // Method to get a specific product by its name from the shop page
  public getProductByName(name: string): Locator {
    return this.allProductItems.filter({ hasText: name });
  }

  // Method to get the "Add to cart" button for a specific product by its name
  public getAddToCartButtonByProductName(name: string): Locator {
    return this.getProductByName(name).locator('a.add_to_cart_button');
  }

  // Method to get the "View cart" button that might appear after adding a product
  public getViewCartButtonByProductName(name: string): Locator {
    return this.getProductByName(name).locator('a.added_to_cart');
  }

  // Sorting dropdown
  public get sortByDropdown(): Locator {
    return this.page.locator('select.orderby');
  }

  // Pagination - "Load more" button or numbered links
  public get loadMoreButton(): Locator { // If applicable
    return this.page.locator('button.load-more'); // Example
  }

  public get paginationLinks(): Locator { // If applicable
    return this.page.locator('.woocommerce-pagination a.page-numbers');
  }

  public get nextPaginationLink(): Locator {
      return this.page.locator('.woocommerce-pagination a.next.page-numbers');
  }


  constructor(page: Page) {
    super(page);
  }

  async waitForPageToLoad(): Promise<void> {
    // Wait for the main shop heading and product grid to be visible
    await expect(this.pageTitleHeading).toBeVisible({ timeout: 10000 });
    await expect(this.productGrid).toBeVisible({ timeout: 10000 });
    // Ensure at least one product item is loaded, if the grid can be initially empty on slow loads
    // await expect(this.allProductItems.first()).toBeVisible({ timeout: 15000 });
  }

  // --- Shop Page Specific Actions ---

  async addProductToCart(productName: string): Promise<void> {
    const addToCartButton = this.getAddToCartButtonByProductName(productName);
    await addToCartButton.click();
    // Wait for the "View cart" link to appear for that product, or a global cart notification
    await expect(this.getViewCartButtonByProductName(productName)).toBeVisible({ timeout: 5000 });
    // or await this.page.waitForLoadState('networkidle');
  }

  async sortProductsBy(optionValueOrLabel: string | { value?: string, label?: string }): Promise<void> {
    if (typeof optionValueOrLabel === 'string') {
        // Try by value first, then by label if direct string match is intended for visible text
        try {
            await this.sortByDropdown.selectOption(optionValueOrLabel);
        } catch (e) {
            await this.sortByDropdown.selectOption({ label: optionValueOrLabel });
        }
    } else {
        await this.sortByDropdown.selectOption(optionValueOrLabel);
    }
    // Wait for products to reload/re-sort
    await this.page.waitForLoadState('networkidle', { timeout: 7000 });
    // Add a more specific wait if possible, e.g., for the first product to potentially change
    // or a loading spinner to disappear.
    await expect(this.productGrid).toBeVisible(); // ensure grid is still there
  }

  async goToNextPageOfProducts(): Promise<void> {
      const nextPageLink = this.nextPaginationLink;
      if (await nextPageLink.isVisible()) {
          await nextPageLink.click();
          await this.waitForPageToLoad(); // Or a more specific wait for content update
      } else {
          console.warn("Next page link not found or not visible.");
      }
  }

  // --- Shop Page Specific Assertions ---

  async assertProductIsVisible(productName: string): Promise<void> {
    await expect(this.getProductByName(productName)).toBeVisible();
  }

  async assertProductHasAddToCartButton(productName: string): Promise<void> {
    await expect(this.getAddToCartButtonByProductName(productName)).toBeVisible();
  }
}
