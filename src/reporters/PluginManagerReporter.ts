import { Reporter, TestCase, TestResult, TestStep, TestError, Suite, FullConfig, FullResult } from '@playwright/test/reporter';
import { TestExecutionPlugin } from '../plugins/TestExecutionPlugin';

export class PluginManagerReporter implements Reporter {
  private plugins: TestExecutionPlugin[];

  constructor(plugins: TestExecutionPlugin[] = []) {
    this.plugins = plugins;
    if (this.plugins.length > 0) {
      console.log(`PluginManagerReporter initialized with ${this.plugins.length} plugin(s).`);
    }
  }

  private async M_callPlugins<K extends keyof TestExecutionPlugin>(
    methodName: K,
    ...args: Parameters<NonNullable<TestExecutionPlugin[K]>>
  ): Promise<void> {
    for (const plugin of this.plugins) {
      if (typeof plugin[methodName] === 'function') {
        try {
          // Cast to any to satisfy TypeScript for calling the function with dynamic args
          // NonNullable<TestExecutionPlugin[K]> ensures that plugin[methodName] is a function if it exists
          await (plugin[methodName] as any)(...args);
        } catch (error) {
          console.error(`Error in plugin ${plugin.constructor.name} during ${methodName}:`, error);
        }
      }
    }
  }

  async onBegin(config: FullConfig, suite: Suite): Promise<void> {
    await this.M_callPlugins('onBegin', config, suite);
  }

  async onEnd(result: FullResult): Promise<void> {
    await this.M_callPlugins('onEnd', result);
  }

  async onTestBegin(test: TestCase, result: TestResult): Promise<void> { // Playwright uses onTestBegin
    await this.M_callPlugins('onTestStart', test, result);
  }

  async onTestEnd(test: TestCase, result: TestResult): Promise<void> {
    await this.M_callPlugins('onTestEnd', test, result);
  }

  async onStepBegin(test: TestCase, result: TestResult, step: TestStep): Promise<void> { // Playwright uses onStepBegin
    // Ensure the step is a user-initiated step if desired (type === 'test.step')
    if (step.category === 'test.step') { // Only top-level steps from test.step()
        await this.M_callPlugins('onStepStart', test, result, step);
    }
  }

  async onStepEnd(test: TestCase, result: TestResult, step: TestStep): Promise<void> {
    if (step.category === 'test.step') {
        await this.M_callPlugins('onStepEnd', test, result, step);
    }
  }

  onError(error: TestError): void | Promise<void> {
    return this.M_callPlugins('onError', error);
  }

  // Other reporter methods (optional, implement if plugins need them)
  // onStdOut(chunk: string | Buffer, test?: TestCase, result?: TestResult): void { ... }
  // onStdErr(chunk: string | Buffer, test?: TestCase, result?: TestResult): void { ... }
  // printsToStdio(): boolean { return true; } // if you want your reporter to log to console via onStdOut/Err

  // Suite-level reporting is tricky with Playwright's Reporter V2 for individual suites.
  // `onBegin` and `onEnd` receive the root suite. Nested suites are part of the `suite.suites` structure.
  // If plugins need to act on individual suites, they might need to traverse `suite.suites` in `onBegin`.
}
