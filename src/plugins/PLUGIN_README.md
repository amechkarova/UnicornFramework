# Test Execution Plugin System

This framework includes a test execution plugin system that allows hooking into various lifecycle events of Playwright's test execution. This is achieved using Playwright's [Custom Reporter API](https://playwright.dev/docs/reporter-api).

## Core Components

1.  **`TestExecutionPlugin` (Interface - `src/plugins/TestExecutionPlugin.ts`)**:
    This interface defines the methods that a plugin can implement to react to test events. These methods largely mirror the events available in Playwright's `Reporter` API (e.g., `onTestStart`, `onTestEnd`, `onStepStart`, `onStepEnd`, `onError`, `onBegin`, `onEnd`).

2.  **`PluginManagerReporter` (Reporter - `src/reporters/PluginManagerReporter.ts`)**:
    This class implements Playwright's `Reporter` interface. Its main responsibilities are:
    - To hold a list of registered `TestExecutionPlugin` instances.
    - To iterate over these plugins and call their corresponding methods when Playwright emits an event (e.g., when `PluginManagerReporter.onTestStart()` is called by Playwright, it calls `onTestStart()` on all registered plugins).

## Creating a New Plugin

1.  **Create your plugin class:**
    - Create a new TypeScript file in the `src/plugins/` directory (e.g., `MyCustomPlugin.ts`).
    - Implement the `TestExecutionPlugin` interface. You only need to implement the methods for the events you care about.

    ```typescript
    // src/plugins/MyCustomPlugin.ts
    import { TestCase, TestResult } from '@playwright/test/reporter';
    import { TestExecutionPlugin } from './TestExecutionPlugin';

    export class MyCustomPlugin implements TestExecutionPlugin {
      async onTestStart(test: TestCase, result: TestResult): Promise<void> {
        console.log(`[MyCustomPlugin] Test starting: ${test.title}`);
      }

      async onTestEnd(test: TestCase, result: TestResult): Promise<void> {
        if (result.status === 'failed') {
          console.log(`[MyCustomPlugin] Test FAILED: ${test.title}`);
          // Example: Send a notification to Slack or log to a dashboard
        }
      }
      // Implement other methods as needed...
    }
    ```

## Registering a Plugin

Plugins are registered in the `playwright.config.ts` file when configuring the `PluginManagerReporter`.

1.  **Import your new plugin** in `playwright.config.ts`.
2.  **Instantiate your plugin** and add it to the array of plugins passed to `PluginManagerReporter`.

    ```typescript
    // playwright.config.ts
    import { PlaywrightTestConfig } from '@playwright/test';
    // ... other imports
    import { PluginManagerReporter } from './src/reporters/PluginManagerReporter';
    import { LoggingPlugin } from './src/plugins/LoggingPlugin';
    import { MyCustomPlugin } from './src/plugins/MyCustomPlugin'; // 1. Import

    const config: PlaywrightTestConfig = {
      reporter: [
        ['list'], // Default list reporter
        // Add our custom reporter with its plugins
        [
          './src/reporters/PluginManagerReporter.ts', // Path to the reporter
          [ // Array of plugin instances
            new LoggingPlugin(),
            new MyCustomPlugin(), // 2. Instantiate and add here
          ]
        ]
      ],
      // ... other configurations
    };

    export default config;
    ```

## Playwright's Built-in Features

Remember that Playwright already provides powerful built-in features like:
-   **Screenshots on failure**: Configured via `use: { screenshot: 'only-on-failure' }`.
-   **Video recording**: Configured via `use: { video: 'retain-on-failure' }`.
-   **Tracing**: A comprehensive tool for debugging tests.

The plugin system is designed to **complement** these features, enabling custom actions such as:
-   Advanced or specialized logging.
-   Sending notifications to external systems (e.g., Slack, Teams).
-   Reporting results to custom dashboards or test management tools.
-   Triggering other processes based on test events.
```
