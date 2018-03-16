using System;
using System.Collections.Generic;
using Mediachase.Commerce;
using Mediachase.Commerce.Core;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.Commerce.Payments.PayPal.Helpers
{
    /// <summary>
    /// Handles PayPal supported currencies.
    /// </summary>
    public class PayPalCurrencies
    {
        private readonly IList<string> _payPalSupportedCurrencies = new List<string>()
        {
            "AUD", "BRL", "CAD",
            "CZK", "DKK", "EUR", "HKD", "HUF", "ILS", "JPY", "MYR",
            "MXN", "NOK", "NZD", "PHP", "PLN", "GBP", "SGD",
            "SEK", "CHF", "TWD", "THB", "USD"
        };

        private readonly SiteContext _siteContext;

        public PayPalCurrencies() : this(SiteContext.Current)
        { }

        public PayPalCurrencies(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }

        /// <summary>
        /// Gets currency code for PayPal
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <returns>PayPal currency code type.</returns>
        public CurrencyCodeType GetCurrencyCode(Currency currency)
        {
            var currencyCode = !string.IsNullOrEmpty(currency) ? currency.CurrencyCode.ToUpperInvariant() : _siteContext.Currency.CurrencyCode.ToUpperInvariant();
            if (_payPalSupportedCurrencies.Contains(currencyCode))
            {
                if (Enum.TryParse(currencyCode, out CurrencyCodeType currencyCodeType))
                {
                    return currencyCodeType;
                }
            }

            return CurrencyCodeType.CUSTOMCODE;
        }
    }
}
