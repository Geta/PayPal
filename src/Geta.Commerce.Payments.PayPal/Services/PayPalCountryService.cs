using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using EPiServer;
using EPiServer.Commerce.Order;
using EPiServer.Framework.Localization;
using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using Geta.PayPal;
using Mediachase.Commerce;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Customers.Profile;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Plugins.Payment;
using Mediachase.Commerce.Security;
using Mediachase.Commerce.Website.Helpers;
using Mediachase.Data.Provider;
using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.Commerce.Payments.PayPal.Services
{
    [ServiceConfiguration(typeof(IPayPalCountryService))]
    public class PayPalCountryService : IPayPalCountryService
    {
        private DataTable _ISOAlpha;
        private DataTable _USstates;
        private static object _lock = new object();

        [NonSerialized]
        private readonly ILogger _logger = LogManager.GetLogger(typeof(PayPalCountryService));

        // TODO change to the appropriate path of the template.
        public string FilePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath(Constants.ModuleFilePath);
            }
        }

        /// <summary>
        /// Gets the country code table, to changes between ISO Alpha2 and Alpha3 contry code
        /// </summary>
        /// <returns></returns>
        /// <value>The country code table.</value>
        public DataTable CountryCodeTable
        {
            get
            {
                if (_ISOAlpha == null)
                {
                    lock (_lock)
                    {
                        if (_ISOAlpha == null)
                        {
                            string isoFilePath = FilePath;
                            if (isoFilePath.EndsWith(@"\"))
                            {
                                isoFilePath += "ISOCodes.txt";
                            }
                            else
                            {
                                isoFilePath += @"\ISOCodes.txt";
                            }

                            try
                            {
                                using (StreamReader reader = new StreamReader(isoFilePath))
                                {
                                    _ISOAlpha = new DataTable();
                                    _ISOAlpha.Columns.Add("ISO3", typeof(string));
                                    _ISOAlpha.Columns.Add("ISO2", typeof(string));
                                    _ISOAlpha.Columns.Add("Name", typeof(string));

                                    string s = reader.ReadLine();
                                    while (!reader.EndOfStream && !string.IsNullOrEmpty(s))
                                    {
                                        string[] values = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        string alpha3 = values[0];
                                        string alpha2 = values[1];
                                        string name = string.Empty;
                                        for (int i = 2; i < values.Length; i++)
                                        {
                                            name += " " + values[i];
                                        }
                                        _ISOAlpha.Rows.Add(new object[] { alpha3, alpha2, name.TrimStart(' ') });
                                        s = reader.ReadLine();
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                _logger.Error("Cannot read from iso mapping file, file does not exist or content is not wellformed",ex);
                            }
                        }
                    }
                }
                return _ISOAlpha;
            }
        }

        /// <summary>
        /// Gets the US and Canadian state codes, for
        /// US and Canadian, PayPal accepts only two letter state code.
        /// </summary>
        /// <value>The U sstates.</value>
        public DataTable USstates
        {
            get
            {
                if (_USstates == null)
                {
                    lock (_lock)
                    {
                        if (_USstates == null)
                        {
                            _USstates = new DataTable();
                            _USstates.Columns.Add("Name", typeof(string));
                            _USstates.Columns.Add("Code", typeof(string));

                            string isoFilePath = FilePath;

                            if (isoFilePath.EndsWith(@"\"))
                            {
                                isoFilePath += "CanadianOrUSstates.txt";
                            }
                            else
                            {
                                isoFilePath += @"\CanadianOrUSstates.txt";
                            }
                            try
                            {
                                using (StreamReader reader = new StreamReader(isoFilePath))
                                {
                                    while (!reader.EndOfStream)
                                    {
                                        string name = reader.ReadLine();
                                        string code = reader.ReadLine();
                                        _USstates.Rows.Add(new object[] { name, code });
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                return _USstates;
            }
        }

        /// <summary>
        /// Gets the alpha3 country code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns></returns>
        public string GetAlpha3CountryCode(string countryCode)
        {
            string name = string.Empty;
            foreach (DataRow row in CountryCodeTable.Rows)
            {
                if (row[1].ToString().ToUpperInvariant() == countryCode.ToUpperInvariant())
                {
                    name = row[0].ToString().ToUpperInvariant();
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// Gets the alpha2 country code.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public CountryCodeType GetAlpha2CountryCode(IOrderAddress address)
        {
            if (CountryCodeTable == null)
            {
                return CountryCodeType.CUSTOMCODE;
            }

            string code = string.Empty;
            foreach (DataRow row in CountryCodeTable.Rows)
            {
                if (row[0].ToString().Equals(address.CountryCode, StringComparison.OrdinalIgnoreCase))
                {
                    code = row[1].ToString().ToUpperInvariant();
                    break;
                }
            }

            foreach (CountryCodeType value in Enum.GetValues(typeof(CountryCodeType)))
            {
                if (value.ToString().ToUpperInvariant() == code)
                {
                    return value;
                }
            }

            return CountryCodeType.CUSTOMCODE;
        }

        /// <summary>
        /// States the name from state code.
        /// </summary>
        /// <param name="stateCode">The state code.</param>
        /// <returns></returns>
        public string GetStateName(string stateCode)
        {
            if (string.IsNullOrEmpty(stateCode))
            {
                return string.Empty;
            }
            foreach (DataRow row in USstates.Rows)
            {
                if (string.Equals(row[1].ToString(), stateCode, StringComparison.OrdinalIgnoreCase))
                {
                    return row[0].ToString();
                }
            }
            return stateCode;
        }

        /// <summary>
        /// States the code from the state name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetStateCode(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            foreach (DataRow row in USstates.Rows)
            {
                if (string.Equals(row[0].ToString(), name, StringComparison.OrdinalIgnoreCase))
                {
                    return row[1].ToString().ToUpperInvariant();
                }
            }
            return name;
        }

        /// <summary>
        /// Try to get the PayPal's currency code from payment's parent BillingCurrency or SiteContext's currencyCode
        /// </summary>
        /// <value>The PayPal currency code.</value>
        public CurrencyCodeType GetCurrencyCode(IPayment payment, Currency orderGroupCurrency)
        {
            CurrencyCodeType currency = CurrencyCodeType.CUSTOMCODE;
            if (string.IsNullOrEmpty(orderGroupCurrency))
            {
                currency = GetCurrencyCode(SiteContext.Current.Currency);
            }
            else
            {
                currency = GetCurrencyCode(orderGroupCurrency);
            }
            return currency;
        }

        /// <summary>
        /// Currency code for PayPal
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <returns></returns>
        private CurrencyCodeType GetCurrencyCode(Currency currency)
        {
            string cur = currency.CurrencyCode.ToUpperInvariant();
            if (Constants.PayPalSupportedCurrencies.Contains(cur))
            {
                foreach (CurrencyCodeType value in Enum.GetValues(typeof(CurrencyCodeType)))
                {
                    if (value.ToString().ToUpperInvariant() == cur)
                    {
                        return value;
                    }
                }
            }
            return CurrencyCodeType.CUSTOMCODE;
        }
    }
}