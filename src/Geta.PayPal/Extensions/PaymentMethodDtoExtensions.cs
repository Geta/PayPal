using Mediachase.Commerce.Orders.Dto;

namespace Geta.PayPal.Extensions
{
    public static class PaymentMethodDtoExtensions
    {
        /// <summary>
        /// Gets the PayPal PaymentMethodDto's parameter (setting in CommerceManager of PayPal) value by name.
        /// </summary>
        /// <param name="paymentMethodDto">The payment method dto</param>
        /// <param name="name">Name of parameter</param>
        /// <returns>string.Empty if not found</returns>
        public static string GetParameterValueByName(this PaymentMethodDto paymentMethodDto, string name)
        {
            var paramRow = paymentMethodDto.GetParameterByName(name);
            if (paramRow != null)
            {
                return paramRow.Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the PayPal PaymentMethodDto's parameter (setting in CommerceManager of PayPal) by name.
        /// </summary>
        /// <param name="paymentMethodDto">The payment method dto</param>
        /// <param name="name">Name of parameter</param>
        /// <returns>null if not found</returns>
        public static PaymentMethodDto.PaymentMethodParameterRow GetParameterByName(this PaymentMethodDto paymentMethodDto, string name)
        {
            var rowArray = (PaymentMethodDto.PaymentMethodParameterRow[])paymentMethodDto.PaymentMethodParameter.Select(string.Format("Parameter = '{0}'", name));
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                return rowArray[0];
            }

            return null;
        }
    }
}