using EPiServer.Commerce.Order;
using EPiServer.Commerce.Order.Internal;

namespace Geta.Commerce.Payments.PayPal.Extensions
{
    public static class IOrderGroupBuilderExtensions
    {
        public static IPayment CreatePayPalPayment(this IOrderGroupBuilder orderGroupBuilder)
        {
            if (orderGroupBuilder is SerializableCartBuilder)
            {
                return new SerializablePayPalPayment();
            }

            return new PayPalPayment();
        }
    }
}