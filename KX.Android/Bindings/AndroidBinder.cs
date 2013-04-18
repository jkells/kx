using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using KX.Core;
using KX.Core.Observables;

namespace KX.Android.Bindings
{
    public class AndroidBinder : KXBinder
    {        
        public AndroidBinder(Type viewModelType) : base(viewModelType)
        {            
        }

        public IEnumerable<Core.Binding> BindLayout(object viewModel, ViewGroup viewGroup)
        {
            var observables = new Dictionary<string, KXObservable>();
            foreach (var item in ObservableProperties)
            {
                var observable = KXObservableBuilder.CreateFor(item.Value.PropertyType);
                observables.Add(item.Key, observable);
                item.Value.SetValue(viewModel, observable, null);
            }

            var views = GetViews(viewGroup).ToList();
            foreach (var item in views)
            {
                var name = ToLowerCaseAlphaOnly(item.Key);
                var view = item.Value;
                if (observables.ContainsKey(name))
                {
                    var textView = view as TextView;
                    if (textView != null)
                    {
                        yield return new AndroidTextViewTextBinding(observables[name], textView);
                    }
                }
            }
        }

        private IEnumerable<KeyValuePair<string, View>> GetViews(ViewGroup viewGroup)
        {
            for (int i = 0; i < viewGroup.ChildCount; i++)
            {
                var view = viewGroup.GetChildAt(i);
                var group = view as ViewGroup;
                if (group != null)
                {                
                    var children = GetViews(group);
                    foreach (var child in children)
                    {
                        yield return child;
                    }                    
                }

                if (view.Id > 0)
                {
                    string name = viewGroup.Context.Resources.GetResourceEntryName(view.Id);
                    yield return new KeyValuePair<string, View>(name, view);
                }
            }
        }
    }
}