using System;
using System.Collections.Generic;
using EPiServer.Commerce.Order;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using Geta.PayPal;
using Geta.PayPal.Extensions;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.Commerce.Payments.PayPal.Services
{
    [ServiceConfiguration(typeof(IPayPalPaymentService))]
    public class PayPalPaymentService : IPayPalPaymentService
    {
        private readonly IPayPalCountryService _countryService;
        private readonly IOrderGroupCalculator _orderGroupCalculator;

        [NonSerialized]
        private readonly ILogger _logger = LogManager.GetLogger(typeof(PayPalCountryService));

        public PayPalPaymentService(IPayPalCountryService countryService, IOrderGroupCalculator orderGroupCalculator)
        {
            _countryService = countryService;
            _orderGroupCalculator = orderGroupCalculator;
        }

        public DoCaptureResponseType CapturePayment(IPayment payment, IOrderGroup orderGroup, out string errorMessage)
        {
            var po = (IPurchaseOrder) orderGroup;
            var captureAmount = payment.Amount;
            var currency = _countryService.GetCurrencyCode(payment, orderGroup.Currency);

            var captureRequest = new DoCaptureRequestType
            {
                AuthorizationID = payment.TransactionID,
                Amount = captureAmount.ToPayPalAmount(currency)
            };

            // original transactionID (which PayPal gave us when DoExpressCheckoutPayment, DoDirectPayment, or CheckOut)
            // if refund with Partial, we have to set the Amount

            captureRequest.CompleteType = payment.Amount >= _orderGroupCalculator.GetTotal(po).Amount 
                ? CompleteCodeType.COMPLETE 
                : CompleteCodeType.NOTCOMPLETE;

            captureRequest.InvoiceID = po.OrderNumber;

            captureRequest.Note = string.Format("[{2}-{3}] captured {0}{1} for [PurchaseOrder-{4}]",
                captureAmount, currency,
                payment.PaymentMethodName, payment.TransactionID,
                po.OrderNumber
                );

            var caller = GetPayPalAPICallerServices();
            var doCaptureReq = new DoCaptureReq
            {
                DoCaptureRequest = captureRequest
            };

            var captureResponse = caller.DoCapture(doCaptureReq);

            errorMessage = captureResponse.CheckErrors();

            return captureResponse;
        }

        /// <summary>
        /// Setup the PayPal API caller service, use the profile setting with pre-configured parameters
        /// </summary>
        /// <returns>null if any of APIUsername, APIPassword, APISignature, Environment is missing</returns>
        public virtual PayPalAPIInterfaceServiceService GetPayPalAPICallerServices()
        {
            var payPal = GetPayPalPaymentMethod();

            var configMap = new Dictionary<string, string>();
            configMap.Add("mode", payPal.GetParameterValueByName(PayPalConstants.Configuration.SandBoxParameter) == "1" ? "sandbox" : "live");

            // Signature Credential
            configMap.Add("account1.apiUsername", payPal.GetParameterValueByName(PayPalConstants.Configuration.UserParameter));
            configMap.Add("account1.apiPassword", payPal.GetParameterValueByName(PayPalConstants.Configuration.PasswordParameter));
            configMap.Add("account1.apiSignature", payPal.GetParameterValueByName(PayPalConstants.Configuration.APISisnatureParameter));

            return new PayPalAPIInterfaceServiceService(configMap);
        }

        /// <summary>
        /// Return the PaymentMethodDto of PayPal
        /// </summary>
        /// <returns>Return PayPal payment method</returns>
        public virtual PaymentMethodDto GetPayPalPaymentMethod()
        {
            PaymentMethodDto oPayPal = PaymentManager.GetPaymentMethodBySystemName(Constants.SystemKeyword, SiteContext.Current.LanguageName);
            return oPayPal;
        }
    }
}