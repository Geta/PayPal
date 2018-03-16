using System;
using System.Collections.Generic;
using System.Web;

namespace Geta.PayPal
{
    public class PayPalConstants
    {
        public static class Configuration
        {
            public const string BusinessEmailParameter = "PayPalBusinessEmail";
            public const string AllowChangeAddressParameter = "PayPalChangeAddress";
            public const string AllowGuestParameter = "PayPalAllowGuest";
            public const string PaymentActionParameter = "PayPalPaymentAction";
            public const string UserParameter = "PayPalAPIUser";
            public const string PasswordParameter = "PayPalAPIPassword";
            public const string APISisnatureParameter = "PayPalAPISisnature";
            public const string PALParameter = "PayPalPAL";
            public const string SandBoxParameter = "PayPalSandBox";
            public const string ExpChkoutURLParameter = "PayPalExpChkoutURL";
            public const string SkipConfirmPageParameter = "SkipConfirmPage";
            public const string SuccessUrl = "PayPalSuccessUrl";
            public const string CancelUrl = "PayPalCancelUrl";
        }

        public static class Keys
        {
            public const string CurrentCartKey = "CurrentCart";
            public const string CurrentContextKey = "CurrentContext";
            public const string PayPalCookieKey = "Sample_PayPal_values";

        }

        public static class Session
        {
            public const string LatestOrderIdKey = "LatestOrderId";
            public const string LatestOrderNumberKey = "LatestOrderNumber";
            public const string LatestOrderTotalKey = "LatestOrderTotal";
        }

        public static class Status
        {
            public const string PaymentStatusCompleted = "PayPal payment completed";
        }
    }
}