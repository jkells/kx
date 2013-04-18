namespace KX.Core.Observables
{
    internal class KXObservableInt : KXObservable<int>
    {
        public override int Get()
        {
            int i;
            if (int.TryParse(StringValue, out i))
                return i;

            return 0;
        }

        public override void Set(int value)
        {
            StringValue = value.ToString();
        }
    }
}