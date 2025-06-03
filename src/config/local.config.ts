import { TestConfig } from './testConfig';

const localConfig: Partial<TestConfig> = {
  baseURL: 'http://localhost:8080', // Override baseURL for local environment
  headless: false, // Run tests in headed mode locally
  // Add other local-specific overrides
};

export default localConfig;
