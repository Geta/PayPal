using System;
using System.Collections.Generic;
using System.Linq;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;

namespace Geta.PayPal
{
    /// <summary>
    /// Represents PayPal configuration data.
    /// </summary>
    public class PayPalConfiguration
    {
        private PaymentMethodDto _paymentMethodDto;
        private IDictionary<string, string> _settings;

        public const string PayPalSystemName = "PayPal";

        public const string BusinessEmailParameter = "PayPalBusinessEmail";
        public const string AllowChangeAddressParameter = "PayPalChangeAddress";
        public const string AllowGuestParameter = "PayPalAllowGuest";
        public const string PaymentActionParameter = "PayPalPaymentAction";
        public const string UserParameter = "PayPalAPIUser";
        public const string PasswordParameter = "PayPalAPIPassword";
        public const string ApiSignatureParameter = "PayPalAPISignature";
        public const string PalParameter = "PayPalPAL";
        public const string SandBoxParameter = "PayPalSandBox";
        public const string ExpChkoutUrlParameter = "PayPalExpChkoutURL";
        public const string SkipConfirmPageParameter = "SkipConfirmPage";
        public const string SuccessUrlParameter = "PayPalSuccessUrl";
        public const string CancelUrlParameter = "PayPalCancelUrl";

        public Guid PaymentMethodId { get; protected set; }

        public string BusinessEmail { get; protected set; }

        public string AllowChangeAddress { get; protected set; }

        public string AllowGuest { get; protected set; }

        public string PaymentAction { get; protected set; }

        public string User { get; protected set; }

        public string Password { get; protected set; }

        public string ApiSignature { get; protected set; }

        public string Pal { get; protected set; }

        public string SandBox { get; protected set; }

        public string ExpChkoutUrl { get; protected set; }

        public string SkipConfirmPage { get; protected set; }

        public string SuccessUrl { get; protected set; }

        public string CancelUrl { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of <see cref="T:Geta.PayPal.PayPalConfiguration" />.
        /// </summary>
        public PayPalConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PayPalConfiguration"/> with specific settings.
        /// </summary>
        /// <param name="settings">The specific settings.</param>
        public PayPalConfiguration(IDictionary<string, string> settings)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Initialize(settings);
        }

        /// <summary>
        /// Gets the PaymentMethodDto's parameter (setting in CommerceManager of PayPal) by name.
        /// </summary>
        /// <param name="paymentMethodDto">The payment method dto.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>The parameter row.</returns>
        public static PaymentMethodDto.PaymentMethodParameterRow GetParameterByName(PaymentMethodDto paymentMethodDto, string parameterName)
        {
            var rowArray = (PaymentMethodDto.PaymentMethodParameterRow[])paymentMethodDto
                .PaymentMethodParameter
                .Select($"Parameter = '{parameterName}'");
            return rowArray.Length > 0 ? rowArray[0] : null;
        }

        /// <summary>
        /// Returns the PaymentMethodDto of PayPal.
        /// </summary>
        /// <returns>The PayPal payment method.</returns>
        public static PaymentMethodDto GetPayPalPaymentMethod()
        {
            return PaymentManager.GetPaymentMethodBySystemName(PayPalSystemName, SiteContext.Current.LanguageName);
        }

        protected virtual void Initialize(IDictionary<string, string> settings)
        {
            _paymentMethodDto = GetPayPalPaymentMethod();
            PaymentMethodId = GetPaymentMethodId();

            _settings = settings ?? GetSettings();
            GetParametersValues();
        }

        private IDictionary<string, string> GetSettings()
        {
            return _paymentMethodDto.PaymentMethod
                                    .FirstOrDefault()
                                   ?.GetPaymentMethodParameterRows()
                                   ?.ToDictionary(row => row.Parameter, row => row.Value);
        }

        private void GetParametersValues()
        {
            if (_settings != null)
            {
                BusinessEmail = GetParameterValue(BusinessEmailParameter);
                AllowChangeAddress = GetParameterValue(AllowChangeAddressParameter);
                AllowGuest = GetParameterValue(AllowGuestParameter);
                PaymentAction = GetParameterValue(PaymentActionParameter);
                User = GetParameterValue(UserParameter);
                Password = GetParameterValue(PasswordParameter);
                ApiSignature = GetParameterValue(ApiSignatureParameter);
                Pal = GetParameterValue(PalParameter);
                SandBox = GetParameterValue(SandBoxParameter);
                ExpChkoutUrl = GetParameterValue(ExpChkoutUrlParameter);
                SkipConfirmPage = GetParameterValue(SkipConfirmPageParameter);
                SuccessUrl = GetParameterValue(SuccessUrlParameter);
                CancelUrl = GetParameterValue(CancelUrlParameter);
            }
        }

        private string GetParameterValue(string parameterName)
        {
            return _settings.TryGetValue(parameterName, out var parameterValue) ? parameterValue : string.Empty;
        }

        private Guid GetPaymentMethodId()
        {
            return _paymentMethodDto.PaymentMethod.Rows[0] is PaymentMethodDto.PaymentMethodRow paymentMethodRow
                ? paymentMethodRow.PaymentMethodId
                : Guid.Empty;
        }
    }
}
