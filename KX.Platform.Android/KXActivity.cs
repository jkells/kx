using System;
using Android.OS;
using KX.Core;
using Xamarin.ActionbarSherlockBinding.App;

namespace KX.Platform.Android
{
    public class KXActivity : SherlockActivity
    {
        private const string StateViewModelType = "vm_type";
        private KXActivityService _service;
        protected Type ViewModelType { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            var modelTypeString = Intent.GetStringExtra(StateViewModelType);
            if (modelTypeString != null)
            {
                var modelType = Type.GetType(modelTypeString, false);

                if (modelType != null)
                    ViewModelType = modelType;
            }

            if (ViewModelType == null)
            {
                throw new KXException("View model type not specified. Unable to start activity");
            }

            _service = new KXActivityService(this, ViewModelType);
            _service.OnCreate(bundle);
            base.OnCreate(bundle);
        }

        protected override void OnSaveInstanceState(Bundle bundle)
        {
            _service.OnSaveInstanceState(bundle);
        }

        protected override void OnDestroy()
        {
            _service.OnDestroy();
        }
    }
}