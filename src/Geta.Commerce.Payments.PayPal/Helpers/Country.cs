namespace Geta.Commerce.Payments.PayPal.Helpers
{
    public class Country
    {
        public string Iso2 { get; }
        public string Iso3 { get; }
        public string Name { get; }

        public Country(string iso2, string iso3, string name)
        {
            Iso2 = iso2;
            Iso3 = iso3;
            Name = name;
        }
    }
}