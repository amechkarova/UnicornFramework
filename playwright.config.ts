import { PlaywrightTestConfig } from '@playwright/test';
import { PluginManagerReporter } from './src/reporters/PluginManagerReporter';
import { LoggingPlugin } from './src/plugins/LoggingPlugin';

const config: PlaywrightTestConfig = {
  reporter: [
    ['list'], // Default list reporter
    // Add our custom reporter
    ['./src/reporters/PluginManagerReporter.ts', [new LoggingPlugin()]]
  ],
  testDir: './tests',
  timeout: 30000,
  use: {
    headless: false,
    viewport: { width: 1280, height: 720 },
    actionTimeout: 10000,
    ignoreHTTPSErrors: true,
    video: 'retain-on-failure',
    screenshot: 'only-on-failure',
  },
  projects: [
    {
      name: 'Chromium',
      use: { browserName: 'chromium' },
    },
    {
      name: 'Firefox',
      use: { browserName: 'firefox' },
    },
    {
      name: 'WebKit',
      use: { browserName: 'webkit' },
    },
  ],
};

export default config;
