import { TestConfig } from './testConfig';
import defaultConfig from './default.config';
import * as path from 'path';
import * as fs from 'fs';

const env = process.env.ENV || 'local'; // Default to 'local' if ENV is not set

let environmentConfig: Partial<TestConfig> = {};

const envConfigFile = path.resolve(__dirname, `${env}.config.ts`);

if (fs.existsSync(envConfigFile)) {
  try {
    const configModule = require(envConfigFile);
    environmentConfig = configModule.default || configModule;
  } catch (error) {
    console.error(`Error loading environment config file: ${envConfigFile}`, error);
    // Optionally, you could throw the error or fall back to defaults only
  }
} else {
  console.warn(`Environment config file not found: ${envConfigFile}. Using default config.`);
}

const mergedConfig: TestConfig = {
  ...defaultConfig,
  ...environmentConfig,
};

export const getConfig = (): TestConfig => {
  return mergedConfig;
};

// Log the final loaded configuration for debugging (optional)
// console.log('Loaded configuration:', getConfig());
