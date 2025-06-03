export interface BillingInfo {
  firstName: string;
  lastName: string;
  companyName?: string; // Optional
  country: string; // This might be a country code or full name depending on the site
  streetAddress1: string;
  streetAddress2?: string; // Optional
  city: string;
  state?: string; // Optional, depends on the country
  zip: string;
  phone: string;
  email: string;
  // Future: add payment details if needed, e.g., creditCardNumber, expiryDate, cvc
}

// Example of creating a BillingInfo object:
// const billingDetails: BillingInfo = {
//   firstName: 'John',
//   lastName: 'Doe',
//   companyName: 'Acme Corp',
//   country: 'US', // Or 'United States'
//   streetAddress1: '123 Main St',
//   city: 'Anytown',
//   state: 'CA', // Or 'California'
//   zip: '90210',
//   phone: '555-123-4567',
//   email: 'john.doe@example.com',
// };
