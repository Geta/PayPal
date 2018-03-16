namespace Geta.Commerce.Payments.PayPal
{
    public interface IPayPalPayment
    {
        string PayPalOrderNumber { get; set; }
        string PayPalExpToken { get; set; }
    }
}