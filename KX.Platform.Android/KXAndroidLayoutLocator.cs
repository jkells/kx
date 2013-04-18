using System;
using KX.Core;

namespace KX.Platform.Android
{
    public class KXAndroidLayoutLocator
    {
        private readonly global::Android.Content.Res.Resources _resources;
        private readonly string _packageName;

        public KXAndroidLayoutLocator(global::Android.Content.Res.Resources resources, string packageName)
        {
            _resources = resources;
            _packageName = packageName;
        }

        public int FindLayout(Type viewModelType)
        {
            var viewModelName = viewModelType.Name;
            if (viewModelName.EndsWith("vm", StringComparison.InvariantCultureIgnoreCase))
            {
                viewModelName = viewModelName.Substring(0, viewModelName.Length - 2);
            }

            var layoutId = _resources.GetIdentifier(viewModelName.ToLowerInvariant(), "layout", _packageName);
            if (layoutId == 0)
                throw new KXException("Unable to find a layout resource file named: " + viewModelName);

            return layoutId;
        }
    }
}