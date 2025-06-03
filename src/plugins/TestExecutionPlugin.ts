import { TestCase, TestResult, TestStep, TestError, Suite, FullConfig, FullResult } from '@playwright/test/reporter';

/**
 * Defines the interface for a test execution plugin.
 * Plugins can hook into various lifecycle events of the test execution process.
 * These methods mirror events available in Playwright's Reporter API.
 */
export interface TestExecutionPlugin {
  /** Called once before running tests. All tests have been loaded and suites tree is ready. */
  onBegin?(config: FullConfig, suite: Suite): Promise<void> | void;

  /** Called after all tests have been run. */
  onEnd?(result: FullResult): Promise<void> | void;

  /** Called when a test is about to start running. */
  onTestStart?(test: TestCase, result: TestResult): Promise<void> | void;

  /** Called when a test finished running. */
  onTestEnd?(test: TestCase, result: TestResult): Promise<void> | void;

  /** Called when a test step is about to start running. */
  onStepStart?(test: TestCase, result: TestResult, step: TestStep): Promise<void> | void;

  /** Called when a test step finished running. */
  onStepEnd?(test: TestCase, result: TestResult, step: TestStep): Promise<void> | void;

  /** Called when an error occurs during test execution. */
  onError?(error: TestError): Promise<void> | void;

  /** Called when a suite is about to start running. (Note: Playwright doesn't have a direct onSuiteStart for individual suites during execution like onTestStart) */
  // onSuiteStart?(suite: Suite): Promise<void> | void; // This is more like onBegin for the root suite. For nested suites, structure is in suite.suites

  /** Called when a suite has finished running. (Note: Playwright doesn't have a direct onSuiteEnd for individual suites) */
  // onSuiteEnd?(suite: Suite): Promise<void> | void; // This is more like onEnd for the root suite.

  // Additional methods for more granular control if needed, mapping to C# observer:
  // preTestInit?(test: TestCase): Promise<void> | void; // Before Playwright's own beforeEach
  // postTestInit?(test: TestCase, result: TestResult): Promise<void> | void; // After Playwright's own beforeEach / start of test body
  // preTestCleanup?(test: TestCase, result: TestResult): Promise<void> | void; // Before Playwright's own afterEach
  // postTestCleanup?(test: TestCase, result: TestResult): Promise<void> | void; // After Playwright's own afterEach
  // For now, these are commented out as they don't directly map to Reporter API events easily without more complex hooking.
}
