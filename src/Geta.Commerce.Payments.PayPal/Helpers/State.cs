namespace Geta.Commerce.Payments.PayPal.Helpers
{
    public class State
    {
        public string Code { get; }
        public string Name { get; }

        public State(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}