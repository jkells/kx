namespace KX.Core.Observables
{
    internal class KXObservableString : KXObservable<string>
    {
        public override string Get()
        {
            return StringValue;
        }

        public override void Set(string value)
        {
            StringValue = value;
        }
    }
}