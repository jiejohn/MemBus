using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using MemBus;
using Membus.WpfTwitterClient.Frame;
using StructureMap;
using System.Linq;

namespace Membus.WpfTwitterClient.Startup
{
    public class AppBootstrap : Bootstrapper<ShellViewModel>
    {
        private IContainer container;

        public AppBootstrap()
        {
            
        }

        protected override void Configure()
        {
            ObjectFactory.Initialize(i => i.AddRegistry<ClientRegistry>());
            ObjectFactory.Configure(c =>
                                        {
                                            c.ForSingletonOf<IWindowManager>().Use(new WindowManager());
                                            c.ForSingletonOf<IEventAggregator>().Use(new EventAggregator());
                                        });
            container = ObjectFactory.Container;
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service).OfType<object>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {Assembly.GetExecutingAssembly()};
        }

        protected override void DisplayRootView()
        {
            var shell = container.GetInstance<ShellViewModel>();
            container.GetInstance<IWindowManager>().Show(shell);

        }

        protected override void PrepareApplication()
        {
            base.PrepareApplication();
            //container.GetInstance<IBus>().Publish(new RequestToStartup());
        }
    }
}