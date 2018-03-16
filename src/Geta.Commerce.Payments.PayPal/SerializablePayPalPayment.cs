using System.Collections;
using EPiServer.Commerce.Order.Internal;
using Mediachase.Commerce.Orders;
using Newtonsoft.Json;

namespace Geta.Commerce.Payments.PayPal
{
    /// <inheritdoc cref="IPayPalPayment" />
    /// <summary>
    /// Represents Serializable Payment class for PayPal.
    /// </summary>
    [JsonConverter(typeof(PaymentConverter))]
    public class SerializablePayPalPayment : SerializablePayment, IPayPalPayment
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Commerce.Payments.PayPal.SerializablePayPalPayment" /> class.
        /// </summary>
        public SerializablePayPalPayment()
        {
            Properties = new Hashtable();
            BillingAddress = new SerializableOrderAddress();
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Commerce.Payments.PayPal.SerializablePayPalPayment" /> class.
        /// </summary>
        [JsonConstructor]
        public SerializablePayPalPayment(SerializableOrderAddress billingAddress)
        {
            BillingAddress = billingAddress;
        }

        /// <summary>
        /// Represents the PayPal order number
        /// </summary>
        public string PayPalOrderNumber { get; set; }

        /// <summary>
        /// Represents the PayPal exp token
        /// </summary>
        public string PayPalExpToken { get; set; }
    }
}