using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geta.Commerce.Payments.PayPal
{
    public static class Constants
    {
        public const string SystemKeyword = "PayPal";

        public const string ModuleFilePath = "\\modules\\_protected\\Geta.Commerce.Payments.PayPal\\";

        public static List<string> PayPalSupportedCurrencies = new List<string>() { "AUD", "BRL", "CAD",
            "CZK", "DKK", "EUR", "HKD", "HUF", "ILS", "JPY", "MYR",
            "MXN", "NOK", "NZD", "PHP", "PLN", "GBP", "SGD",
            "SEK", "CHF", "TWD", "THB", "USD"
        };

        public static List<string> PayPalSupportedCountriesAndRegions = new List<string>() { "AF", "AX","AL","DZ","AS", "AD", "AO" , "AI" ,"AQ" ,
            "AG" ,"AR" ,"AM" ,"AW" ,"AU" ,"AT" ,"AZ" ,"BS" ,"BH" ,"BD" ,"BB" ,"BY" , "BE" , "BZ" ,"BJ","BM","BT","BO","BA" ,
            "BW" ,"BV" ,"BR" ,"IO" ,"BN" ,"BG" ,"BF" ,"BI" ,"KH" ,"CM" ,"CA" ,"CV" ,"KY" ,"CF" ,"TD" ,"CL" ,"CN" ,"CX" ,"CC" ,"CO" ,"KM" ,"CG" ,"CD" ,
            "CK" ,"CR" ,"CI" ,"HR" ,"CU" ,"CY", "CZ" ,"DK", "DJ" ,"DM" ,"DO" ,"EC" ,"EG" ,"SV" ,"GQ" ,"ER" ,"EE" ,"ET" ,"FK" ,"FO" ,"FJ" ,"FI" ,"FR" ,
            "GF" ,"PF" ,"TF" ,"GA" ,"GM" ,"GE" ,"DE" ,"GH" ,"GI" ,"GR" ,"GL" ,"GD" ,"GP" ,"GU" ,"GT" ,"GG" ,"GN" ,"GW" ,"GY" ,"HT" ,"HM" ,"VA" ,"HN" ,
            "HK" ,"HU" ,"IS" ,"IN" ,"ID" ,"IR" ,"IQ" ,"IE" ,"IM" ,"IL" ,"IT" ,"JM" ,"JP" ,"JE" ,"JO" ,"KZ" ,"KE" ,"KI" ,"KP" ,"KR" ,"KW" ,"KG", "LA" ,
            "LV" ,"LB" ,"LS" ,"LR" ,"LY" ,"LI" ,"LT" ,"LU" ,"MO" ,"MK" ,"MG" ,"MW" ,"MY" ,"MV" ,"ML" ,"MT" ,"MH" ,"MQ" ,"MR" ,"MU" ,"YT" ,"MX","FM" ,"MD" ,
            "MC" ,"MN" ,"MS" ,"MA" ,"MZ" ,"MM" ,"NA" ,"NR" ,"NP" ,"NL" ,"AN" ,"NC" ,"NZ" ,"NI" ,"NE" ,"NG" ,"NU" ,"NF" ,"MP" ,"NO" ,"OM" ,"PK" ,"PW" ,"PS" ,
            "PA" ,"PG" ,"PY" ,"PE" ,"PH" ,"PN" ,"PL" ,"PT" ,"PR" ,"QA" ,"RE" ,"RO", "RU" ,"RW" ,"SH" ,"KN" ,"LC" ,"PM" ,"VC" ,"WS" ,"SM" ,"ST" ,"SA" ,"SN" ,
            "CS" ,"SL","SG" ,"SK" ,"SI" ,"SB","SO" ,"ZA" ,"GS" ,"ES" ,"LK" ,"SD","SR","SJ","SZ" ,"SE" ,"CH" ,"SY","TW" ,"TJ" ,"TZ" ,"TH" ,"TL" ,"TG" ,"TK" ,
            "TO" ,"TT" ,"TN" ,"TR" ,"TM" ,"TC" ,"TV" ,"UG" ,"UA" ,"AE" ,"GB" ,"US" ,"UM" ,"UY" ,"UZ" ,"VU" ,"VE" ,"VN" ,"VG" ,"VI" ,"WF" ,"EH" ,"YE" ,"ZM" , "ZW" };
    }
}