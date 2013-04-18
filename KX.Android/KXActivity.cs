using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using KX.Android.Bindings;
using KX.Core;
using Xamarin.ActionbarSherlockBinding.App;

namespace KX.Android
{
    public class KXActivity : SherlockActivity
    {
        private Type _applicationType;
        private KXApplication _application;
        private object _viewModel;
        private readonly List<Core.Binding> _bindings = new List<Core.Binding>();

        private const string StateViewModelType = "vm_type";
        private Type ViewModelType
        {
            get
            {
                string typeName = Intent.GetStringExtra(StateViewModelType);
                if (typeName == null)
                {
                    return _application.InitialViewModel;
                }

                return Type.GetType(typeName);
            }
        }

        protected void SetApplicationType(Type type)
        {
            _applicationType = type;
        }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CreateApplication();
            CreateViewModel();
            CreateLayout();
            CreateBindings();
            InitModel();
        }

        private void InitModel()
        {
            var init = _viewModel as IInitialize;
            if (init != null)
            {
                init.Init();
            }
        }

        protected override void OnDestroy()
        {
            foreach (var binding in _bindings)
            {
                binding.Dispose();   
            }            
        }

        private void CreateBindings()
        {
            var view = Window.DecorView.RootView as ViewGroup;
            
            var binder = new AndroidBinder(_viewModel.GetType());
            _bindings.AddRange(binder.BindLayout(_viewModel, view));
        }
        
        private void CreateLayout()
        {
            var viewModelName = _viewModel.GetType().Name;
            if (viewModelName.EndsWith("vm", StringComparison.InvariantCultureIgnoreCase))
            {
                viewModelName = viewModelName.Substring(0, viewModelName.Length - 2);
            }

            var layoutId = Resources.GetIdentifier(viewModelName.ToLowerInvariant(), "layout", ApplicationContext.PackageName);
            if (layoutId == 0)
                throw new KXException("Unable to find a layout resource file named: " + viewModelName);

            SetContentView(layoutId);
        }

        private void CreateViewModel()
        {
            Type viewModelType = ViewModelType;
            _viewModel = Activator.CreateInstance(viewModelType);
        }

        private void CreateApplication()
        {
            if(_applicationType == null)
                throw new KXException("Application type must be specified by calling SetApplicationType()");

            object application = Activator.CreateInstance(_applicationType);
            _application = application as KXApplication;
            if (_application == null)
            {
                throw new KXException("Application object must be assignable to KXApplication");
            }
        }
    }
}