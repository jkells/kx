using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KX.Core.Observables;
using TinyIoC;

namespace KX.Core.IoC
{
    public class KXResolver
    {
        public TinyIoCContainer Container { get; private set; }
        private static KXResolver _current;

        public static KXResolver Current
        {
            get
            {
                if (_current == null)
                    throw new KXException("Container not initialized");

                return _current;
            }
        }

        public static void Create(IEnumerable<Assembly> assemblies)
        {
            if (_current == null)
            {
                _current = new KXResolver(assemblies);
            }
        }

        private KXResolver(IEnumerable<Assembly> assemblies)
        {
            Container = new TinyIoCContainer();
            RegisterCoreTypes();
            var bootstraps =assemblies.SelectMany(assembly => assembly.SafeGetTypes())
                      .Where(type => typeof (IBootstrap).IsAssignableFrom(type));

            foreach (var bootstrapType in bootstraps)
            {
                var bootstrap = Activator.CreateInstance(bootstrapType) as IBootstrap;
                if (bootstrap != null) 
                    bootstrap.RegisterTypes(Container);
            }
        }       

        private void RegisterCoreTypes()
        {
            Container.Register<KXObservable<string>>((container, overloads) => new KXObservableString());
            Container.Register<KXObservable<int>>((container, overloads) => new KXObservableInt());
        }
    }
}