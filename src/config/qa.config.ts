import { TestConfig } from './testConfig';

const qaConfig: Partial<TestConfig> = {
  baseURL: 'https://qa.example.com', // Override baseURL for QA environment
  headless: true, // Run tests in headless mode in QA
  // Add other QA-specific overrides
};

export default qaConfig;
