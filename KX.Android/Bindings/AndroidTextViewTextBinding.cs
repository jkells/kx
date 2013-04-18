using Android.Widget;
using KX.Core;
using KX.Core.Observables;

namespace KX.Android.Bindings
{
    public class AndroidTextViewTextBinding : Binding
    {
        private bool _disposed;
        private readonly Subscription _subscription;

        public AndroidTextViewTextBinding(KXObservable observable, TextView textView)
        {
            _subscription = observable.Subscribe();
            _subscription.OnChange = value => textView.Text = value;

        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _subscription.Dispose();
                    _disposed = true;
                }                
            }
        }
    }
}