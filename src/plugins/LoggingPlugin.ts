import { TestCase, TestResult, TestStep, Suite, FullConfig, FullResult, TestError } from '@playwright/test/reporter';
import { TestExecutionPlugin } from './TestExecutionPlugin';

export class LoggingPlugin implements TestExecutionPlugin {
  onBegin(config: FullConfig, suite: Suite): void {
    console.log(`[LoggingPlugin] Test run starting. Total tests: ${suite.allTests().length}`);
    // console.log(`[LoggingPlugin] Root suite title: ${suite.title}`);
    // this.logSuiteStructure(suite); // Optional: log the full suite structure
  }

  private logSuiteStructure(suite: Suite, indent = 0) {
    const prefix = '  '.repeat(indent);
    // console.log(`${prefix}Suite: ${suite.title} (Tests: ${suite.tests.length}, Suites: ${suite.suites.length})`);
    suite.tests.forEach(test => {
      // console.log(`${prefix}  Test: ${test.title}`);
    });
    suite.suites.forEach(s => this.logSuiteStructure(s, indent + 1));
  }

  onEnd(result: FullResult): void {
    console.log(`[LoggingPlugin] Test run finished. Status: ${result.status}`);
  }

  onTestStart(test: TestCase, result: TestResult): void {
    const relativePath = test.location.file.split('tests/')[1] || test.location.file;
    console.log(`[LoggingPlugin] Test Started: ${test.title} (${relativePath}:${test.location.line})`);
  }

  onTestEnd(test: TestCase, result: TestResult): void {
    const duration = result.duration;
    const errors = result.errors.map(e => e.message).join('\n');
    console.log(`[LoggingPlugin] Test Ended: ${test.title} - Status: ${result.status} (${duration}ms)`);
    if (errors) {
      console.log(`[LoggingPlugin] Errors: \n${errors}`);
    }
    // Log attachments like screenshots or videos if any
    // result.attachments.forEach(attachment => {
    //   if (attachment.name === 'screenshot' && attachment.path) {
    //     console.log(`[LoggingPlugin]   Screenshot: ${attachment.path}`);
    //   }
    //   if (attachment.name === 'video' && attachment.path) {
    //     console.log(`[LoggingPlugin]   Video: ${attachment.path}`);
    //   }
    // });
  }

  onStepStart(test: TestCase, result: TestResult, step: TestStep): void {
    if (step.category === 'test.step') { // Ensure it's a user-defined step
      console.log(`[LoggingPlugin]   Step Started: ${step.title} (Parent: ${test.title})`);
    }
  }

  onStepEnd(test: TestCase, result: TestResult, step: TestStep): void {
    if (step.category === 'test.step') {
      const error = step.error ? ` - Error: ${step.error.message}` : '';
      console.log(`[LoggingPlugin]   Step Ended: ${step.title} (Parent: ${test.title}, Duration: ${step.duration}ms${error})`);
    }
  }

  onError(error: TestError): void {
    console.error(`[LoggingPlugin] Global Error: ${error.message}\n${error.stack || ''}\n${error.value || ''}`);
  }
}
