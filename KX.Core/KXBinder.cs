using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using KX.Core.Observables;

namespace KX.Core
{
    public abstract class KXBinder
    {
        private readonly Type _viewModelType;
        protected Dictionary<string, PropertyInfo> ObservableProperties = new Dictionary<string, PropertyInfo>();

        protected KXBinder(Type viewModelType)
        {
            _viewModelType = viewModelType;
            ObservableProperties = GetWriteablePropertiesOfType<KXObservable>()
                .ToDictionary(item => ToLowerCaseAlphaOnly(item.Name), item => item);
        }

        private IEnumerable<PropertyInfo> GetWriteablePropertiesOfType<T>()
        {
            return _viewModelType
                .GetProperties()
                .Where(p => typeof(T).IsAssignableFrom(p.PropertyType))
                .Where(p => p.CanWrite);
        }
        
        protected static string ToLowerCaseAlphaOnly(string token)
        {
            var sb = new StringBuilder();

            foreach (var c in token)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLowerInvariant(c));
                }
            }
            return sb.ToString();
        }
    }
}