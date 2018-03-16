using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Order;
using Mediachase.Commerce.Orders.Dto;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.Commerce.Payments.PayPal.Services
{
    public interface IPayPalPaymentService
    {
        DoCaptureResponseType CapturePayment(IPayment payment, IOrderGroup orderGroup, out string errorMessage);

        // temporary
        PaymentMethodDto GetPayPalPaymentMethod();

        // temporary
        PayPalAPIInterfaceServiceService GetPayPalAPICallerServices();
    }
}