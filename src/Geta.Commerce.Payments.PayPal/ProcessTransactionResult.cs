using EPiServer.Commerce.Order;

namespace Geta.Commerce.Payments.PayPal
{
    public class ProcessTransactionResult
    {
        public IPurchaseOrder PurchaseOrder { get; set; } 
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}