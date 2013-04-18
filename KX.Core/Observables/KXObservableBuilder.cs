using System;

namespace KX.Core.Observables
{
    public class KXObservableBuilder
    {
        public static KXObservable CreateFor<T>()
        {
            return CreateFor(typeof (T));
        }

        public static KXObservable CreateFor(Type propertyType)
        {
            if (propertyType.IsAssignableFrom(typeof(KXObservableInt)))
            {
                return new KXObservableInt();
            }

            if (propertyType.IsAssignableFrom(typeof(KXObservableString)))
            {
                return new KXObservableString();
            }

            throw new KXException("Unknown binding type: " + propertyType.FullName); 
        }
    }
}
