using System;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Views;
using KX.Core;
using KX.Core.IoC;
using KX.Platform.Android.Bindings;

namespace KX.Platform.Android
{
    public class KXActivityService
    {     
        private readonly Activity _activity;
        private readonly Type _viewModelType;
        private AndroidBinder _binder;
        private readonly KXAndroidLayoutLocator _layoutLocator;
        public object ViewModel { get; private set; }       

        public KXActivityService(Activity activity, Type viewModelType)
        {            
            _activity = activity;
            _viewModelType = viewModelType;

            var packageName = _activity.ApplicationContext.PackageName;
            _layoutLocator = new KXAndroidLayoutLocator(_activity.Resources, packageName);
            CreateContainer();
        }

        public void OnCreate(Bundle savedInstanceState)
        {
            KXResolver.Current.Container.Register(_viewModelType);
            ViewModel = KXResolver.Current.Container.Resolve(_viewModelType);
            KXResolver.Current.Container.BuildUp(ViewModel);

            CreateLayout();
            _binder = new AndroidBinder(_viewModelType);
            _binder.BindLayout(ViewModel, (ViewGroup) _activity.Window.DecorView.RootView);
            
            InitModel();
        }
        
        public void OnSaveInstanceState(Bundle bundle)
        {
            
        }

        private void CreateLayout()
        {
            var layoutId = _layoutLocator.FindLayout(_viewModelType);
            _activity.SetContentView(layoutId);
        }

        public void OnDestroy()
        {
            _binder.Dispose();
            _binder = null;
        }

        private void InitModel()
        {
            var init = ViewModel as IKXReady;
            if (init != null)
            {
                init.Ready();
            }
        }

        private void CreateContainer()
        {
            Assembly kxAndroid = GetType().Assembly;
            Assembly androidAppAssembly = _activity.GetType().Assembly;
            KXResolver.Create(new[] { kxAndroid, androidAppAssembly });
        }
    }
}