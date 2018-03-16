using System.Linq;
using EPiServer.Commerce.Order;

namespace Geta.PayPal.Extensions
{
    public static class IPurchaseOrderExtensions
    {
        /// <summary>
        /// Update display name with current language
        /// </summary>
        /// <param name="po">The purchase order</param>
        public static void UpdateDisplayNameWithCurrentLanguage(this IPurchaseOrder po)
        {
            if (po != null)
            {
                if (po.Forms != null && po.Forms.Count > 0)
                {
                    foreach (IOrderForm orderForm in po.Forms)
                    {
                        var lineItems = orderForm.GetAllLineItems();
                        if (lineItems != null && lineItems.Any())
                        {
                            foreach (var item in lineItems)
                            {
                                item.DisplayName = item.GetDisplayNameOfCurrentLanguage(100);
                            }
                        }
                    }
                }
            }
        }
    }
}