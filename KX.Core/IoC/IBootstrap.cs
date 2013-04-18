using TinyIoC;

namespace KX.Core.IoC
{
    public interface IBootstrap
    {
        void RegisterTypes(TinyIoCContainer container);
    }
}
