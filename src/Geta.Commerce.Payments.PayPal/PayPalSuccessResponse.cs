namespace Geta.Commerce.Payments.PayPal
{
    public class PayPalAcceptResponse
    {
        public bool Accept { get; set; } 
        public string Hash { get; set; }
        public string Token { get; set; }
        public string PayerId { get; set; }
    }
}