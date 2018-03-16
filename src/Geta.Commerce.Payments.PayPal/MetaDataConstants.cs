namespace Geta.Commerce.Payments.PayPal
{
    public static class MetadataConstants
    {
        public const string OrderNamespace = "Mediachase.Commerce.Orders";
        public const string UsersNameSpace = "Mediachase.Commerce.Orders.User";

        public const string OrderFormPaymentClass = "OrderFormPayment";

        public const string PayPalClassName = "PayPalPayment";
        public const string PayPalFriendlyName = "PayPal payment";
        public const string PayPalTableName = "OrderFormPaymentEx_PayPalPayment";

        public static string PayPalExpToken = "PayPalExpToken";
        public static string PayPalOrderNumber = "PayPalOrderNumber";
    }
}