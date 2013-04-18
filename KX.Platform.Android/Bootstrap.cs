using KX.Core.IoC;
using TinyIoC;

namespace KX.Platform.Android
{
    class Bootstrap : IBootstrap
    {
        public void RegisterTypes(TinyIoCContainer container)
        {
            container.Register<KXAndroidNavigate>();
        }
    }
}