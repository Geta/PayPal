PayPal payment provider for EPiServer Commerce
=============


## What is Geta.EPi.Commerce.Payments.PayPal?

Geta.EPi.Commerce.Payments.PayPal is based on Episerver's own payment provider for PayPal which is downloadable for older versions of Episerver Commerce. This implementation is upgraded to 11.2.2. 

It consits of:

* Geta.Commerce.Payments.PayPal
* Geta.Commerce.Payments.PayPal.Manager
* Geta.PayPal


## Setup

### Installation

- Install _Geta.Commerce.Payments.PayPal.Manager_ into _Commerce Manager_ project.
- Install _Geta.Commerce.Payments.PayPal_ into your Web project.
- Add a payment option and payment specific views (see [demo](./demo) project - PayPalPaymentOption, _PayPalConfirmation.cshtml, _PayPalPaymentMethod.cshtml).
- Add redirect if the payment result requires it (see [demo](./demo) project - CheckoutService.PlaceOrder
- Add callback actions to the checkout controller (see [demo](./demo) project - CheckoutController.ProcessPayPalPayment and CheckoutController.FinishPaypalTransaction.

### Commerce Manager

Login into Commerce Manager and open **Administration -> Order System -> Payments**. Then click **New** and in **Overview** tab fill:

- **Name**
- **System Keyword** -  The System keyword must be "PayPal" 
- **Language**
- **Class Name** - choose **Geta.Commerce.Payments.PayPal.PayPalPaymentGateway**
- **Payment Class** - choose **Geta.Commerce.Payments.PayPal.PayPalPayment**
- **IsActive** - **Yes**
- Select shipping methods available for this payment
- Navigate to parameters tab and fill settings 

### Parameters tab (API settings)
- **Business email** - 
- **API Username** - 
- **Password** -
- **API Signature** - 
- **Use test environment (sandbox)** -
- **Allow buyers to change shipping address at PayPal** -
- **Payment action** -
- **Allow guest checkout** - 
- **Express Checkout Url** - https://www.sandbox.paypal.com/cgi-bin/webscr
- **Success URL** - /en/checkout/ProcessPayPalPayment
- **Cancel URL** - /en/checkout/ProcessPayPalPayment
- **Skip OrderConfirmation page** - 
- **PayPal Secure Merchant Account ID (PAL) (optional)** -


**Note: If the parameters tab is empty, make sure you have installed the commerce manager nuget**

In **Markets** tab select market for which this payment will be available.