using Android.Text;
using Android.Widget;
using KX.Core;
using KX.Core.Observables;

namespace KX.Platform.Android.Bindings
{
    internal class EditTextViewTextBinding : KXBinding
    {
        private readonly KXObservable _observable;
        private readonly TextView _textView;
        private bool _disposed;
        private readonly Subscription _subscription;

        public EditTextViewTextBinding(KXObservable observable, TextView textView)
        {
            _observable = observable;
            _textView = textView;

            _subscription = observable.Subscribe();
            _subscription.OnChange = OnModelChange;
            textView.AfterTextChanged += OnViewChange;
        }

        private void OnViewChange(object sender, AfterTextChangedEventArgs e)
        {
            string newValue = e.Editable.ToString();
            _observable.StringValue = newValue;
        }

        private void OnModelChange(string newValue)
        {            
// ReSharper disable RedundantCheckBeforeAssignment
            if (_textView.Text != newValue)
// ReSharper restore RedundantCheckBeforeAssignment
            {
                _textView.Text = newValue;
            }
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