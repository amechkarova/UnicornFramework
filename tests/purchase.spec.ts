import { test, expect } from '../src/fixtures/customFixtures';
import { HomePage } from '../src/pages/HomePage';
import { CartPage } from '../src/pages/CartPage';
import { CheckoutPage } from '../src/pages/CheckoutPage';
import { BillingInfo } from '../src/data/BillingInfo';
import { ShopPage } from '../src/pages/ShopPage'; // Import ShopPage if direct shop interaction is needed

// Test data - consider moving to a separate test data file or generating dynamically if complex
const billingInfoJohnDoe: BillingInfo = {
  firstName: 'John',
  lastName: 'Doe',
  companyName: 'Playwright Testers Inc.',
  country: 'United States', // Full name for label selection
  streetAddress1: '123 Playwright Ave',
  streetAddress2: 'Apt 4B',
  city: 'Testville',
  state: 'California', // Full name for label selection
  zip: '90210',
  phone: '1234567890',
  email: `john.doe.${Date.now()}@example.com`, // Ensure unique email for each run
};

const billingInfoJaneDoe: BillingInfo = {
    firstName: 'Jane',
    lastName: 'Doe',
    companyName: 'Automation Experts LLC',
    country: 'Germany',
    streetAddress1: 'Hauptstrasse 10',
    city: 'Berlin',
    zip: '10115',
    phone: '0987654321',
    email: `jane.doe.${Date.now()}@example.com`,
    // Note: Germany might not require a 'state' in the same way US does.
    // The CheckoutPage.fillBillingInfo handles state visibility.
  };

const falcon9 = 'Falcon 9';
const saturnV = 'Saturn V';

test.describe('Purchase Scenarios @purchase', () => {
  let homePage: HomePage;
  let cartPage: CartPage;
  let checkoutPage: CheckoutPage;
  let shopPage: ShopPage;

  test.beforeEach(async ({ page }) => {
    // Page objects are instantiated before each test
    homePage = new HomePage(page);
    cartPage = new CartPage(page);
    checkoutPage = new CheckoutPage(page);
    shopPage = new ShopPage(page); // Initialize if used in tests directly
  });

  test('should add Falcon 9 to cart and complete purchase successfully with cash on delivery', async ({ page, testConfig }) => {
    test.info().annotations.push({ type: 'test_id', description: 'ATP-001' });
    test.info().annotations.push({ type: 'requirement', description: 'REQ-001: Successful Purchase' });

    await homePage.open(); // Navigates to base URL + homePage.url ('/')
    await homePage.addItemToCart(falcon9);
    await homePage.assertMessageAlertText(new RegExp(`“${falcon9}” has been added to your cart.`));
    await homePage.openCartFromNotification();

    await cartPage.waitForPageToLoad();
    // Example of asserting product details in cart if needed
    // await cartPage.assertProductPrice(falcon9, /\$[0-9,]+\.\d{2}/); // Regex for price format
    await cartPage.proceedToCheckout();

    await checkoutPage.waitForPageToLoad();
    await checkoutPage.fillBillingInfo(billingInfoJohnDoe);
    await checkoutPage.selectPaymentMethod('cod'); // Select Cash on Delivery
    await checkoutPage.placeOrder();

    // Assert order received and details
    await checkoutPage.assertOrderReceived(/Thank you. Your order has been received./i);
    // Further assertions can be added for order details, e.g., on an "Order Details" page if it exists
    // or by checking order summary on the confirmation page.
    console.log(`Test 'should add Falcon 9 to cart and complete purchase' completed. Email: ${billingInfoJohnDoe.email}`);
  });

  test('should apply a valid coupon and see discounted price in cart', async ({ page, testConfig }) => {
    test.info().annotations.push({ type: 'test_id', description: 'ATP-002' });
    test.info().annotations.push({ type: 'requirement', description: 'REQ-002: Coupon Application' });

    const couponCode = 'BELLATRIX10'; // Example coupon code - ensure this is valid on the test site
    const expectedDiscountMessage = /Coupon code applied successfully./i;
    // Note: Price assertions would require knowing the product price and discount amount.
    // This test focuses on the coupon application mechanism.

    // This test might start by adding a product from the main shop page
    await shopPage.open(); // Navigates to base URL + shopPage.url ('/shop/')
    await shopPage.addProductToCart(saturnV); // Add Saturn V from the shop page
    await shopPage.navigateToCartViaMenu(); // Use common nav method from NavigatableShopPage

    await cartPage.waitForPageToLoad();
    const initialTotal = await cartPage.totalSpan.textContent();
    console.log(`Initial cart total: ${initialTotal}`);

    await cartPage.applyCoupon(couponCode);
    await cartPage.assertCouponAppliedSuccessfully(expectedDiscountMessage);

    // Assert that the total price is now different (less) than the initial total.
    // This requires parsing prices, which can be complex due to currency symbols and formatting.
    const newTotalText = await cartPage.totalSpan.textContent();
    console.log(`New cart total after coupon: ${newTotalText}`);
    expect(newTotalText).not.toEqual(initialTotal); // Basic check that total changed

    // More robust price assertion would involve:
    // 1. Reading product price.
    // 2. Knowing coupon discount percentage/amount.
    // 3. Calculating expected total.
    // 4. Comparing formatted strings or numerical values.
    // e.g. await cartPage.assertTotalPrice(/\$[0-9,]+\.\d{2}/); // Check format
  });

  test('should fail to apply an invalid coupon', async ({ page, testConfig }) => {
    test.info().annotations.push({ type: 'test_id', description: 'ATP-003' });
    test.info().annotations.push({ type: 'requirement', description: 'REQ-003: Invalid Coupon' });

    const invalidCouponCode = 'INVALIDCODE123';
    // The exact error message depends on the specific e-commerce platform (WooCommerce).
    // Common messages: "Coupon "INVALIDCODE123" does not exist!" or "Invalid coupon code."
    const expectedErrorMessage = new RegExp(`Coupon "${invalidCouponCode.toLowerCase()}" does not exist!`, 'i');


    await shopPage.open();
    await shopPage.addProductToCart(falcon9);
    await shopPage.navigateToCartViaMenu();

    await cartPage.waitForPageToLoad();
    await cartPage.applyCoupon(invalidCouponCode);
    await cartPage.assertCouponFailed(expectedErrorMessage);
  });

  // Add more tests as needed:
  // - Purchase multiple different items
  // - Update quantity in cart and checkout
  // - Remove item from cart and proceed
  // - Attempt checkout with incomplete billing info (form validation)
  // - Test different payment methods if available
});
