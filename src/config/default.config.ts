import { TestConfig } from './testConfig';

const defaultConfig: TestConfig = {
  baseURL: 'http://localhost:3000', // Default base URL
  browserName: 'chromium',
  headless: true,
  viewport: { width: 1920, height: 1080 },
  actionTimeout: 15000,
  ignoreHTTPSErrors: true,
  video: 'retain-on-failure',
  screenshot: 'only-on-failure',
};

export default defaultConfig;
