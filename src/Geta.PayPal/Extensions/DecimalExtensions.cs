using System.Globalization;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.PayPal.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// Convert value to PayPal amount type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currencyId"></param>
        /// <returns>a basic amount type of PayPal, with 2 decimal digits value</returns>
        public static BasicAmountType ToPayPalAmount(this decimal value, CurrencyCodeType currencyId)
        {
            return new BasicAmountType() { value = value.ToString("0.00", CultureInfo.InvariantCulture), currencyID = currencyId };
        }
    }
}