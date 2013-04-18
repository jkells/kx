using Android.Widget;
using KX.Core;
using KX.Core.Observables;

namespace KX.Platform.Android.Bindings
{
    internal class TextViewTextBinding : KXBinding
    {
        private bool _disposed;
        private readonly Subscription _subscription;

        public TextViewTextBinding(KXObservable observable, TextView textView)
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