import { Browser, BrowserContext, Page, chromium, firefox, webkit } from '@playwright/test';
import { TestConfig } from '../config/testConfig';

export class BrowserManager {
  private browser: Browser | null = null;
  private context: BrowserContext | null = null;
  private page: Page | null = null;
  private config: TestConfig;

  constructor(config: TestConfig) {
    this.config = config;
  }

  async initializeBrowser(): Promise<Browser> {
    if (!this.browser) {
      switch (this.config.browserName) {
        case 'firefox':
          this.browser = await firefox.launch({ headless: this.config.headless });
          break;
        case 'webkit':
          this.browser = await webkit.launch({ headless: this.config.headless });
          break;
        case 'chromium':
        default:
          this.browser = await chromium.launch({ headless: this.config.headless });
          break;
      }
    }
    return this.browser;
  }

  async createNewContext(): Promise<BrowserContext> {
    if (!this.browser) {
      await this.initializeBrowser();
    }
    this.context = await this.browser!.newContext({
      baseURL: this.config.baseURL,
      viewport: this.config.viewport,
      ignoreHTTPSErrors: this.config.ignoreHTTPSErrors,
      // video: this.config.video ? { mode: this.config.video, dir: './test-results/videos' } : undefined, // Simplified, Playwright handles video path in config
      // screenshot: this.config.screenshot ? { mode: this.config.screenshot, fullPage: true } : undefined, // Simplified, Playwright handles screenshot path
    });
    return this.context;
  }

  async createNewPage(): Promise<Page> {
    if (!this.context) {
      await this.createNewContext();
    }
    this.page = await this.context!.newPage();
    if (this.config.actionTimeout) {
      this.page.setDefaultTimeout(this.config.actionTimeout);
    }
    return this.page;
  }

  async getPage(): Promise<Page> {
    if (!this.page) {
      return this.createNewPage();
    }
    return this.page;
  }

  async closePage(): Promise<void> {
    if (this.page) {
      await this.page.close();
      this.page = null;
    }
  }

  async closeContext(): Promise<void> {
    if (this.context) {
      await this.context.close();
      this.context = null;
      this.page = null; // Page is also invalid if context is closed
    }
  }

  async closeBrowser(): Promise<void> {
    if (this.browser) {
      await this.browser.close();
      this.browser = null;
      this.context = null;
      this.page = null;
    }
  }
}
