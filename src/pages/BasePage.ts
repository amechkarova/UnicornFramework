import { Page } from '@playwright/test';

export class BasePage {
  protected readonly page: Page;

  constructor(page: Page) {
    this.page = page;
  }

  // Example of a generic helper method that might be useful
  async getTitle(): Promise<string> {
    return this.page.title();
  }

  async pause(milliseconds: number): Promise<void> {
    await this.page.waitForTimeout(milliseconds);
  }

  // Add other generic helper methods here if needed in the future.
  // For example, methods for handling common pop-ups, iframes, or global notifications.
}
