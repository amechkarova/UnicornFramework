import { test as baseTest } from '@playwright/test';
import { BrowserManager } from '../browser/BrowserManager';
import { getConfig } from '../config/configLoader';
import { TestConfig } from '../config/testConfig';

// Define the types for our custom fixtures
interface CustomFixtures {
  testConfig: TestConfig;
  browserManager: BrowserManager;
  page: import('@playwright/test').Page; // Use import type for Page
}

// Extend the base Playwright test object with our custom fixtures
export const test = baseTest.extend<CustomFixtures>({
  // Fixture for TestConfig
  // Scope: 'worker' ensures this is initialized once per worker process
  testConfig: [async ({}, use) => {
    const config = getConfig();
    // console.log('Fixture: Initialized TestConfig:', config); // Optional: for debugging
    await use(config);
    // No specific teardown needed for config object itself
  }, { scope: 'worker' }],

  // Fixture for BrowserManager
  // Scope: 'worker' ensures BrowserManager is initialized once per worker process
  // and the same browser instance is potentially reused by tests in the same worker.
  browserManager: [async ({ testConfig }, use) => {
    const browserManager = new BrowserManager(testConfig);
    // console.log('Fixture: Initialized BrowserManager with browser:', testConfig.browserName); // Optional: for debugging
    await browserManager.initializeBrowser(); // Initialize browser once
    await use(browserManager);
    // Teardown: Close the browser after all tests in the worker are done
    // console.log('Fixture: Tearing down BrowserManager, closing browser.'); // Optional: for debugging
    await browserManager.closeBrowser();
  }, { scope: 'worker' }],

  // Fixture for Playwright Page
  // Scope: 'test' ensures a new page (and context) is created for each test
  page: async ({ browserManager, testConfig }, use) => {
    // console.log('Fixture: Creating new page and context.'); // Optional: for debugging
    await browserManager.createNewContext(); // Create a new context for each test
    const page = await browserManager.createNewPage();

    // Example of usage: Navigate to baseURL if specified.
    // Alternatively, tests can handle navigation themselves.
    // if (testConfig.baseURL) {
    //   await page.goto(testConfig.baseURL);
    // }

    await use(page);

    // Teardown: Close the page and context after each test
    // console.log('Fixture: Tearing down page and context.'); // Optional: for debugging
    await browserManager.closePage();
    await browserManager.closeContext();
  },
});

// Re-export everything from Playwright test for convenience, so users can import `test` and `expect` from this file.
export { expect } from '@playwright/test';

/**
 * How to use these custom fixtures in your test files:
 *
 * 1. Import `test` and `expect` from this custom fixtures file instead of '@playwright/test'.
 *
 *    ```typescript
 *    // tests/example.spec.ts
 *    import { test, expect } from '../src/fixtures/customFixtures';
 *
 *    test.describe('My Test Suite', () => {
 *      test('should navigate to the homepage', async ({ page, testConfig }) => {
 *        // 'page' is the Playwright Page object, managed by BrowserManager
 *        // 'testConfig' is the loaded configuration
 *
 *        console.log('Base URL from config:', testConfig.baseURL);
 *        await page.goto('/'); // Navigates to baseURL + '/'
 *
 *        // Example assertion
 *        await expect(page).toHaveTitle(/Example Domain/); // Replace with your actual title
 *      });
 *
 *      test('should do something else', async ({ page }) => {
 *        // Another test using the same page fixture, which will get a fresh page and context
 *        await page.goto('/another-page');
 *        // ...
 *      });
 *    });
 *    ```
 *
 * 2. The `page` fixture provides a Playwright `Page` object that is automatically created
 *    before each test and cleaned up afterwards (context and page are closed).
 *    The `BrowserManager` and the browser instance are managed at the worker level,
 *    meaning they are initialized once per worker and torn down when the worker exits.
 *
 * 3. The `testConfig` fixture provides the loaded test configuration, which you can use
 *    to access baseURL, browser settings, etc., within your tests.
 *
 * Note on Scopes:
 * - `testConfig` and `browserManager` are worker-scoped: They are set up once per test worker.
 *   All tests running in the same worker will share the same `TestConfig` instance and the same `BrowserManager` (and thus the same browser instance).
 * - `page` is test-scoped: A new `BrowserContext` and `Page` are created for each individual test,
 *   ensuring test isolation.
 */
