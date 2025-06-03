# Test Structure Guidelines for Playwright Framework

This document provides guidelines on how to structure tests, manage setup/teardown, and utilize shared resources within this Playwright TypeScript framework. We prefer Playwright's built-in mechanisms (fixtures, hooks) over a traditional BaseTest class inheritance model.

## 1. Using Custom Fixtures

Our framework provides custom fixtures defined in `src/fixtures/customFixtures.ts`. These are the primary way to get access to essential test resources.

- **`page`**: A Playwright `Page` object, configured and managed by our `BrowserManager`. It's test-scoped, meaning each test gets a fresh page and browser context.
- **`testConfig`**: The loaded `TestConfig` object (from `src/config/configLoader.ts`). It's worker-scoped, meaning it's loaded once per worker.
- **`browserManager`**: The `BrowserManager` instance. It's worker-scoped. (Typically, you'll interact with `page` directly, but `browserManager` is available if needed for more advanced scenarios, though direct use in tests should be rare).

**Example:**

```typescript
// tests/example.spec.ts
import { test, expect } from '../src/fixtures/customFixtures'; // Import from our custom file
import { HomePage } from '../src/pages/HomePage';

test.describe('My Test Suite', () => {
  let homePage: HomePage;

  // Use beforeEach for objects that need the 'page' fixture from the test
  test.beforeEach(async ({ page }) => {
    homePage = new HomePage(page);
  });

  test('should navigate to the homepage and verify title', async ({ page, testConfig }) => {
    // 'page' and 'testConfig' are automatically provided by the fixture
    console.log('Current environment base URL:', testConfig.baseURL);

    await homePage.open(); // Assuming HomePage has an open method
    await expect(page).toHaveTitle(/Bellatrix Demo Site/); // Replace with actual title part
  });

  test('another test using the same page setup', async ({ page }) => {
    // 'page' here is a new, isolated page instance for this test
    await page.goto(testConfig.baseURL + '/my-account'); // Example using testConfig
    // ... perform actions and assertions
  });
});
```

## 2. Setup and Teardown Hooks

Playwright provides hooks for managing setup and teardown logic at different scopes. These are preferred over virtual methods in a base test class.

- **`test.beforeEach(async ({ page, otherFixtures... }) => { ... })`**:
    - Runs before each test in a `test.describe` block or file.
    - Ideal for:
        - Initializing page objects.
        - Navigating to a common starting URL for a suite of tests.
        - Setting up specific test data required by each test in a suite.

- **`test.afterEach(async ({ page, otherFixtures... }) => { ... })`**:
    - Runs after each test, even if the test fails.
    - Ideal for:
        - Cleaning up test data created by a test (e.g., deleting created entities via API).
        - Logging custom information after each test.
        - The `page` fixture automatically handles closing the page and context.

- **`test.beforeAll(async ({ browserName, playwright, request, config... }) => { ... })`**:
    - Runs once before all tests in a `test.describe` block or file (within a single worker process).
    - **Note:** `beforeAll` does *not* have access to test-scoped fixtures like `page`. It has access to worker-scoped fixtures.
    - Ideal for:
        - Setting up global test data for a suite (e.g., creating a user account via API that all tests in the suite will use).
        - Starting a server or service needed by the tests.
        - Our `testConfig` and `browserManager` fixtures effectively act as worker-scoped `beforeAll` and `afterAll` for their respective functionalities.

- **`test.afterAll(async ({ browserName, playwright, request, config... }) => { ... })`**:
    - Runs once after all tests in a `test.describe` block or file (within a single worker process).
    - Also does *not* have access to test-scoped fixtures.
    - Ideal for:
        - Global cleanup of data created in `beforeAll`.
        - Stopping services started in `beforeAll`.

**Example with Hooks:**

```typescript
// tests/another-suite.spec.ts
import { test, expect } from '../src/fixtures/customFixtures';
import { CartPage } from '../src/pages/CartPage'; // Assuming CartPage exists

test.describe('Shopping Cart Suite', () => {
  let cartPage: CartPage;

  test.beforeAll(async () => {
    console.log('Setting up data for the Shopping Cart Suite...');
    // Example: Create a prerequisite via API if needed by all tests in this suite
  });

  test.beforeEach(async ({ page }) => {
    cartPage = new CartPage(page);
    await cartPage.open(); // Navigate to cart page before each test
  });

  test('should display empty cart message initially', async () => {
    await cartPage.assertCartIsEmpty();
  });

  test('should allow adding item to cart (example, full flow elsewhere)', async ({ page }) => {
    // This test would likely start from HomePage or ShopPage to add an item,
    // then navigate to CartPage or use cartPage methods.
    // For simplicity, assuming an item was added and we are verifying on CartPage.
    // await homePage.addItemToCart('Some Product');
    // await cartPage.open(); // or await homePage.navigateToCart();
    await expect(cartPage.totalSpan).toBeVisible(); // Just an example assertion
  });

  test.afterEach(async () => {
    console.log(`Finished test: ${test.info().title}`);
    // Example: Clean up specific data created by this test, if necessary
  });

  test.afterAll(async () => {
    console.log('Cleaning up data for the Shopping Cart Suite...');
    // Example: Delete data created in beforeAll
  });
});
```

## 3. Accessing Test Information (`TestInfo`)

Playwright's `TestInfo` object provides metadata about the current test, similar to NUnit's `TestContext`.

- It can be accessed by declaring it as a parameter in test functions or fixture functions.
- Useful properties: `testInfo.title`, `testInfo.status`, `testInfo.outputPath()`, `testInfo.attach()`, `testInfo.retry`, etc.

**Example:**

```typescript
test('my test with info', async ({ page }, testInfo) => {
  console.log(`Running test: ${testInfo.title}`);
  if (testInfo.retry > 0) {
    console.log(`This is retry number ${testInfo.retry}`);
  }
  // ... test logic ...
  if (testInfo.status === 'failed') {
    const screenshotPath = testInfo.outputPath('failure-screenshot.png');
    await page.screenshot({ path: screenshotPath });
    testInfo.attachments.push({ name: 'failure-screenshot', contentType: 'image/png', path: screenshotPath });
  }
});
```

## 4. Comparison with C# `BaseTest` Virtual Methods

- **`ClassInit()` (NUnit `OneTimeSetUp`)**: Use `test.beforeAll()` or worker-scoped fixtures.
- **`TestInit()` (NUnit `SetUp`)**: Use `test.beforeEach()` or test-scoped fixtures.
- **`TestCleanup()` (NUnit `TearDown`)**: Use `test.afterEach()` or test-scoped fixture teardown logic.
- **`ClassCleanup()` (NUnit `OneTimeTearDown`)**: Use `test.afterAll()` or worker-scoped fixture teardown logic.

By leveraging Playwright's fixtures and hooks, we achieve flexible and clear test structures without needing a shared `BaseTest` class hierarchy. This approach aligns well with Playwright's execution model and JavaScript/TypeScript conventions.
```
