using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using KX.Core;
using KX.Core.Observables;
using KX.Core.Util;

namespace KX.Platform.Android.Bindings
{
    internal class AndroidBinder : KXBinder
    {
        private bool _disposed;
        private readonly List<KXBinding> _bindings = new List<KXBinding>();

        public AndroidBinder(Type viewModelType) : base(viewModelType)
        {            
        }

        public void BindLayout(object viewModel, ViewGroup viewGroup)
        {    
            var views = GetViews(viewGroup).ToList();
            foreach (var item in views)
            {
                var name = ToLowerCaseAlphaOnly(item.Key);
                var view = item.Value;
                if (ObservableProperties.ContainsKey(name))
                {
                    var observable = ObservableProperties[name].GetValue(viewModel, null) as KXObservable;
                    if (observable != null)
                    {
                        CreateBinding(view, observable);
                    }
                }
            }
        }

        private void CreateBinding(View view, KXObservable observable)
        {
            var editText = view as EditText;
            if (editText != null)
            {
                _bindings.Add(new EditTextViewTextBinding(observable, editText));
                return;
            }

            var textView = view as TextView;
            if (textView != null)
            {
                _bindings.Add(new TextViewTextBinding(observable, textView));
                return;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _bindings.DisposeAll();
                    _bindings.Clear();
                    _disposed = true;
                }                
            }
            base.Dispose(disposing);
        }
    }
}