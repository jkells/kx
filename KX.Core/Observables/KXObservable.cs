using System.Collections.Generic;

namespace KX.Core.Observables
{
    public abstract class KXObservable
    {
        private readonly List<Subscription> _subscribers = new List<Subscription>();
        private string _stringValue;

        public string StringValue
        {
            get { return _stringValue; }
            set
            {
                _stringValue = value;

                foreach (var subscriber in _subscribers)
                {
                    subscriber.Notify();
                }
            }
        }

        public Subscription Subscribe()
        {
            var subscription = new Subscription(this, subscription2 => _subscribers.Remove(subscription2));
            _subscribers.Add(subscription);
            return subscription;
        }
    }

    public abstract class KXObservable<T> : KXObservable
    {
        public abstract T Get();
        public abstract void Set(T value);
    }
}