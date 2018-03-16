using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using EPiServer;
using Geta.PayPal;

namespace Geta.Commerce.Payments.PayPal.Models
{
    public class UrlSettings
    {
        public string AcceptUrl { get; set; }
        public string CancelUrl { get; set; }
        public string NotifyUrl { get; set; }
        public static UrlSettings Create(string orderNumberId, IDictionary<string, string> settings)
        {
            var urlSettings = new UrlSettings();

            // Build notify Url (PayPal will redirect back to this url after processing OK or FAIL)

            // This key is sent to PayPal using https so it is not likely it will come from other because 
            // only PayPal knows this key to send back to us
            string acceptSecurityKey = GetMD5Key(orderNumberId + "accepted");
            string cancelSecurityKey = GetMD5Key(orderNumberId + "canceled");

            string notifyUrl = UriSupport.AbsoluteUrlBySettings(settings[PayPalConstants.Configuration.SuccessUrl]);
            

            string acceptUrl = UriSupport.AddQueryString(notifyUrl, "accept", "true");
            acceptUrl = UriSupport.AddQueryString(acceptUrl, "hash", acceptSecurityKey);
            
            var cancelUrl = UriSupport.AbsoluteUrlBySettings(settings[PayPalConstants.Configuration.CancelUrl]);

            cancelUrl = UriSupport.AddQueryString(cancelUrl, "accept", "false");
            cancelUrl = UriSupport.AddQueryString(cancelUrl, "hash", cancelSecurityKey);

            urlSettings.NotifyUrl = notifyUrl;
            urlSettings.AcceptUrl = acceptUrl;
            urlSettings.CancelUrl = cancelUrl;

            return urlSettings;
        }

        /// <summary>
        /// Gets the MD5 key, in combination with this.HashKey
        /// </summary>
        /// <param name="hashString">The hash string.</param>
        /// <returns></returns>
        public static string GetMD5Key(string hashString)
        {
            MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
            byte[] arrBytes = Encoding.UTF8.GetBytes(HashKey + hashString);
            arrBytes = md5Crypto.ComputeHash(arrBytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in arrBytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the PayPalHashKey from AppSettings. 
        /// If this property is set, this hashkey will be use to hash the share token (between our site and PayPal.com, use when call API to PayPal.com)
        /// </summary>
        /// <value>The hash key.</value>
        internal static string HashKey
        {
            get
            {
                if (!String.IsNullOrEmpty(WebConfigurationManager.AppSettings["PayPalHashKey"]))
                {
                    return WebConfigurationManager.AppSettings["PayPalHashKey"];
                }
                else
                {
                    return _hashKey;
                }
            }
        }
        private const string _hashKey = "@&*PrivateHashKey!%<>?";
    }
}