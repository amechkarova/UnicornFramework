export interface TestConfig {
  baseURL: string;
  browserName: 'chromium' | 'firefox' | 'webkit';
  headless: boolean;
  viewport?: { width: number; height: number };
  actionTimeout?: number;
  ignoreHTTPSErrors?: boolean;
  video?: 'off' | 'on' | 'retain-on-failure' | 'on-first-retry';
  screenshot?: 'off' | 'on' | 'only-on-failure';
  // Add other relevant configuration settings
}
