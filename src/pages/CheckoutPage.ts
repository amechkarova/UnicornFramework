import { Page, Locator, expect } from '@playwright/test';
import { NavigatableShopPage } from './NavigatableShopPage';
import { BillingInfo } from '../data/BillingInfo';

export class CheckoutPage extends NavigatableShopPage {
  public readonly url: string = '/checkout/';

  // --- Locators for Billing Information ---
  public get billingFirstNameField(): Locator { return this.page.locator('#billing_first_name'); }
  public get billingLastNameField(): Locator { return this.page.locator('#billing_last_name'); }
  public get billingCompanyField(): Locator { return this.page.locator('#billing_company'); }
  public get billingCountryDropDown(): Locator { return this.page.locator('#billing_country'); } // This is a select element
  public get billingStreetAddress1Field(): Locator { return this.page.locator('#billing_address_1'); }
  public get billingStreetAddress2Field(): Locator { return this.page.locator('#billing_address_2'); }
  public get billingCityField(): Locator { return this.page.locator('#billing_city'); }
  public get billingStateDropDown(): Locator { return this.page.locator('#billing_state'); } // This is a select element
  public get billingZipField(): Locator { return this.page.locator('#billing_postcode'); }
  public get billingPhoneField(): Locator { return this.page.locator('#billing_phone'); }
  public get billingEmailField(): Locator { return this.page.locator('#billing_email'); }

  // --- Locators for Payment and Order ---
  // Assuming "Cash on delivery" is a payment option. Adapt if others are primary.
  public get paymentMethodCashOnDeliveryRadio(): Locator { return this.page.locator("input[id='payment_method_cod']"); }
  // Add other payment method locators as needed, e.g., #payment_method_stripe, #payment_method_paypal
  public get placeOrderButton(): Locator { return this.page.locator('#place_order'); }

  // --- Locators for Messages ---
  public get orderReceivedMessage(): Locator {
    // The "Thank you. Your order has been received." message is usually within a <p> tag inside .woocommerce-thankyou-order-received
    return this.page.locator('.woocommerce-thankyou-order-received');
  }
  public get paymentErrorMessage(): Locator {
    // Payment errors are often displayed in a .woocommerce-error list
    return this.page.locator('.woocommerce-error li').first(); // Get the first error message
  }

  // --- Locators for Order Summary ---
  public get orderTotal(): Locator { return this.page.locator('.order-total .woocommerce-Price-amount'); }
  // Example: get a product price from the order summary table
  public getProductPriceInOrderSummary(productName: string): Locator {
    return this.page.locator(`//tr[contains(@class, 'cart_item') and ./td[@class='product-name' and contains(., '${productName}')]]/td[@class='product-total']//span[@class='woocommerce-Price-amount']`);
  }


  constructor(page: Page) {
    super(page);
  }

  async waitForPageToLoad(): Promise<void> {
    await expect(this.page.locator("h1:has-text('Checkout')")).toBeVisible({ timeout: 10000 });
    await expect(this.billingFirstNameField).toBeVisible({ timeout: 10000 });
    await expect(this.placeOrderButton).toBeVisible();
  }

  // --- Page Actions ---

  async fillBillingInfo(billingInfo: BillingInfo): Promise<void> {
    await this.billingFirstNameField.fill(billingInfo.firstName);
    await this.billingLastNameField.fill(billingInfo.lastName);
    if (billingInfo.companyName) {
      await this.billingCompanyField.fill(billingInfo.companyName);
    }
    // Handle country dropdown
    // Use { label: billingInfo.country } if country name is displayed, or { value: billingInfo.country } if it's a code.
    await this.billingCountryDropDown.selectOption({ label: billingInfo.country });
    // Wait for potential dynamic loading after country selection (e.g., states)
    await this.page.waitForLoadState('networkidle', { timeout: 3000 });


    await this.billingStreetAddress1Field.fill(billingInfo.streetAddress1);
    if (billingInfo.streetAddress2) {
      await this.billingStreetAddress2Field.fill(billingInfo.streetAddress2);
    }
    await this.billingCityField.fill(billingInfo.city);

    // Handle state dropdown if it's visible/required (depends on country)
    const isStateVisible = await this.billingStateDropDown.isVisible();
    if (billingInfo.state && isStateVisible) {
        // Check if the state field is an input or a select
        const stateTagName = await this.billingStateDropDown.evaluate(el => el.tagName);
        if (stateTagName === 'SELECT') {
            await this.billingStateDropDown.selectOption({ label: billingInfo.state });
        } else { // Assuming it's an input field if not a select
            await this.billingStateDropDown.fill(billingInfo.state);
        }
        await this.page.waitForLoadState('networkidle', { timeout: 3000 });
    } else if (!billingInfo.state && isStateVisible && await this.billingStateDropDown.getAttribute('required')) {
        console.warn("State is required but not provided in billing info.");
        // Potentially throw error or handle as per test case requirement
    }


    await this.billingZipField.fill(billingInfo.zip);
    await this.billingPhoneField.fill(billingInfo.phone);
    await this.billingEmailField.fill(billingInfo.email);
  }

  async selectPaymentMethod(paymentMethodId: 'cod' | 'stripe' | 'paypal' /* add more as needed */): Promise<void> {
    switch(paymentMethodId) {
        case 'cod':
            await this.paymentMethodCashOnDeliveryRadio.check();
            break;
        // Add cases for other payment methods
        // case 'stripe':
        //   await this.page.locator('#payment_method_stripe').check();
        //   break;
        default:
            throw new Error(`Payment method ${paymentMethodId} is not supported.`);
    }
    // Wait for any potential dynamic updates after selecting payment method
    await this.page.waitForLoadState('networkidle', { timeout: 2000 });
  }

  async placeOrder(): Promise<void> {
    await this.placeOrderButton.click();
    // Wait for navigation to order received page or for an error message.
    // Playwright's click auto-waits for navigation if one is triggered.
    // If not a direct navigation, need to wait for specific elements.
    // For this site, placing order navigates to an order-received page or reloads with error.
    // We can wait for either the success message or an error message.
    await this.page.waitForURL(/order-received|\/checkout\//, { timeout: 20000 }); // wait for URL change or stay on checkout (error)
  }

  // --- Assertion Methods ---

  async assertOrderReceived(expectedMessage: string | RegExp = /Thank you. Your order has been received./i): Promise<void> {
    // The order received page typically has a specific URL like /checkout/order-received/ORDER_ID/?key=wc_order_KEY
    await expect(this.page).toHaveURL(/order-received/);
    await expect(this.orderReceivedMessage).toContainText(expectedMessage);
  }

  async assertPaymentError(expectedMessage: string | RegExp): Promise<void> {
    await expect(this.paymentErrorMessage).toBeVisible();
    await expect(this.paymentErrorMessage).toContainText(expectedMessage);
  }

  async assertOrderTotal(expectedTotal: string | RegExp): Promise<void> {
    await expect(this.orderTotal).toBeVisible();
    await expect(this.orderTotal).toHaveText(expectedTotal);
  }

  async assertBillingInfoPrepopulated(expectedBillingInfo: Partial<BillingInfo>): Promise<void> {
    if (expectedBillingInfo.firstName) {
      await expect(this.billingFirstNameField).toHaveValue(expectedBillingInfo.firstName);
    }
    if (expectedBillingInfo.lastName) {
      await expect(this.billingLastNameField).toHaveValue(expectedBillingInfo.lastName);
    }
    if (expectedBillingInfo.companyName) {
      await expect(this.billingCompanyField).toHaveValue(expectedBillingInfo.companyName);
    }
    if (expectedBillingInfo.country) {
        // For select, toHaveValue checks the value attribute of the selected option.
        // If checking by label, you'd need a custom approach or check selected option's text.
      await expect(this.billingCountryDropDown).toHaveValue(expectedBillingInfo.country); // Assumes country is a value code
    }
    if (expectedBillingInfo.streetAddress1) {
      await expect(this.billingStreetAddress1Field).toHaveValue(expectedBillingInfo.streetAddress1);
    }
    if (expectedBillingInfo.city) {
      await expect(this.billingCityField).toHaveValue(expectedBillingInfo.city);
    }
    if (expectedBillingInfo.state) {
        const stateTagName = await this.billingStateDropDown.evaluate(el => el.tagName);
        if (stateTagName === 'SELECT') {
            await expect(this.billingStateDropDown).toHaveValue(expectedBillingInfo.state); // Assumes state is a value code
        } else {
            await expect(this.billingStateDropDown).toHaveValue(expectedBillingInfo.state);
        }
    }
    if (expectedBillingInfo.zip) {
      await expect(this.billingZipField).toHaveValue(expectedBillingInfo.zip);
    }
    if (expectedBillingInfo.phone) {
      await expect(this.billingPhoneField).toHaveValue(expectedBillingInfo.phone);
    }
    if (expectedBillingInfo.email) {
      await expect(this.billingEmailField).toHaveValue(expectedBillingInfo.email);
    }
  }
}
