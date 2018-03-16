using PayPal.PayPalAPIInterfaceService.Model;

namespace Geta.PayPal.Extensions
{
    public static class AbstractResponseTypeExtensions
    {
        /// <summary>
        /// Check the PayPal API response for errors.
        /// </summary>
        /// <param name="abstractResponse">the PayPal API response</param>
        /// <returns>if abstractResponse.Ack is not Success or SuccessWithWarning, return the error message list(s). If everything OK, return string.empty</returns>
        public static string CheckErrors(this AbstractResponseType abstractResponse)
        {
            string errorList = string.Empty;

            // First, check the Obvious.  Make sure Ack is not Success
            if (abstractResponse.Ack != AckCodeType.SUCCESS && abstractResponse.Ack != AckCodeType.SUCCESSWITHWARNING)
            {
                errorList = string.Format("PayPal API {0}.{1}: [{2}] CorrelationID={3}.\n",
                    abstractResponse.Version, abstractResponse.Build,
                    abstractResponse.Ack.ToString(),
                    abstractResponse.CorrelationID
                    // The value returned in CorrelationID is important for PayPal to determine the precise cause of any error you might encounter. 
                    // If you have to troubleshoot a problem with your requests, capture the value of CorrelationID so you can report it to PayPal.
                    );

                if (abstractResponse.Errors.Count > 0)
                {
                    foreach (ErrorType error in abstractResponse.Errors)
                    {
                        errorList += string.Format("\n[{0}-{1}]: {2}.", error.SeverityCode.ToString(), error.ErrorCode, error.LongMessage);
                    }
                }
                else
                {
                    errorList += "Unknown error while calling PayPal API.";   // TODO: localize
                }
            }

            return errorList;
        }
    }
}