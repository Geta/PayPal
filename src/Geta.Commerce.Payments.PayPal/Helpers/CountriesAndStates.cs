using System;
using System.Linq;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.Commerce.Payments.PayPal.Helpers
{
    /// <summary>
    /// Handles countries and states process.
    /// </summary>
    public static class CountriesAndStates
    {
        /// <summary>
        /// Gets the Alpha 3 name by country alpha 2 code.
        /// </summary>
        /// <param name="countryAlpha2Code">The country alpha 2 code.</param>
        /// <returns>The Alpha 3 name.</returns>
        public static string GetAlpha3CountryCode(string countryAlpha2Code)
        {
            var country = Countries.All.FirstOrDefault(c => c.Iso2.Equals(countryAlpha2Code, StringComparison.OrdinalIgnoreCase));
            return country != null ? country.Iso3 : string.Empty;
        }

        /// <summary>
        /// Gets <see cref="CountryCodeType"/> by country alpha 3 code.
        /// </summary>
        /// <param name="countryAlpha3Code">The country alpha 3 code.</param>
        /// <returns>The <see cref="CountryCodeType"/>.</returns>
        public static CountryCodeType GetAlpha2CountryCode(string countryAlpha3Code)
        {
            var country = Countries.All.FirstOrDefault(c => c.Iso3.Equals(countryAlpha3Code, StringComparison.OrdinalIgnoreCase));
            var code = country != null ? country.Iso2 : string.Empty;

            if (string.IsNullOrEmpty(code))
            {
                return CountryCodeType.CUSTOMCODE;
            }

            if (Enum.TryParse<CountryCodeType>(code, out var result))
            {
                return result;
            }

            return CountryCodeType.CUSTOMCODE;
        }

        /// <summary>
        /// Gets the US or Canadian state name by state code.
        /// </summary>
        /// <param name="stateCode">The state code.</param>
        /// <returns>The state name.</returns>
        public static string GetStateName(string stateCode)
        {
            if (string.IsNullOrEmpty(stateCode))
            {
                return string.Empty;
            }

            var state = CanadianUsStates.All.FirstOrDefault(s => s.Code.Equals(stateCode, StringComparison.OrdinalIgnoreCase));
            return state != null ? state.Name : stateCode;
        }

        /// <summary>
        /// Gets the US or Canadian state code by name.
        /// </summary>
        /// <param name="stateName">The state name.</param>
        /// <returns>The state code.</returns>
        public static string GetStateCode(string stateName)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return string.Empty;
            }

            var state = CanadianUsStates.All.FirstOrDefault(s => s.Name.Equals(stateName, StringComparison.OrdinalIgnoreCase));
            return state != null ? state.Code : stateName;
        }
    }
}