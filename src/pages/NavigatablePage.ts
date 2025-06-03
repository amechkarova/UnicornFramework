import { Page } from '@playwright/test';
import { BasePage } from './BasePage';

export abstract class NavigatablePage extends BasePage {
  public abstract readonly url: string;

  constructor(page: Page) {
    super(page);
  }

  public async open(urlSuffix: string = ''): Promise<void> {
    // If urlSuffix is provided, append it. Otherwise, use the page's default url.
    // This allows navigating to page variants like /home or /home?query=abc
    // It assumes this.url is a relative path. If it can be absolute, logic might need adjustment.
    const targetUrl = `${this.url}${urlSuffix}`;
    await this.page.goto(targetUrl);
    await this.waitForPageToLoad();
  }

  /**
   * Abstract method to be implemented by subclasses.
   * This method should contain page-specific logic to wait for essential elements
   * of the page to be loaded and visible, ensuring the page is ready for interaction.
   *
   * Example implementation in a subclass:
   * ```typescript
   * import { expect } from '@playwright/test';
   *
   * async waitForPageToLoad(): Promise<void> {
   *   await expect(this.page.locator('h1')).toBeVisible({ timeout: 10000 }); // Wait for main heading
   *   await expect(this.uniqueFooterElementLocator).toBeVisible();
   * }
   * ```
   */
  public abstract waitForPageToLoad(): Promise<void>;
}
