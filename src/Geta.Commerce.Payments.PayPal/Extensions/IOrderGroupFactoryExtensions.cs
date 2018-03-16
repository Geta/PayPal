using EPiServer.Commerce.Order;

namespace Geta.Commerce.Payments.PayPal.Extensions
{
    public static class IOrderGroupFactoryExtensions
    {
        public static IPayment CreatePayPalPayment(this IOrderGroupFactory orderFactory, IOrderGroup orderGroup)
        {
            return orderFactory.BuilderFor(orderGroup).CreatePayPalPayment();
        }
    }
    
}