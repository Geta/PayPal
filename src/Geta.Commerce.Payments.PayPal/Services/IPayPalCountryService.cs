using EPiServer.Commerce.Order;
using Mediachase.Commerce;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.Commerce.Payments.PayPal.Services
{
    public interface IPayPalCountryService
    {
        string GetStateName(string stateCode);
        string GetStateCode(string name);

        CountryCodeType GetAlpha2CountryCode(IOrderAddress address);
        string GetAlpha3CountryCode(string countryCode);
        CurrencyCodeType GetCurrencyCode(IPayment payment, Currency orderGroupCurrency);
    }
}