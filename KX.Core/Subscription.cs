using System;
using KX.Core.Observables;

namespace KX.Core
{
    public sealed class Subscription : IDisposable
    {
        private readonly KXObservable _observable;
        private readonly Action<Subscription> _removeAction;

        public Subscription(KXObservable observable, Action<Subscription> removeAction)
        {
            _observable = observable;
            _removeAction = removeAction;
        }

        public Action<string> OnChange { get; set; }

        public void Dispose()
        {
            _removeAction(this);
        }

        public void Notify()
        {
            if (OnChange != null)
                OnChange(_observable.StringValue);
        }
    }
}