using System;
using Autofac;

namespace boss.client.win
{
    public static class ApplicationContext
    {
        private static readonly Lazy<IComponentContext> componentContext = new Lazy<IComponentContext>(CreateContainer);

        public static IComponentContext ComponentContext => componentContext.Value;

        private static IComponentContext CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RemoteApplicationService>().As<IApplicationService>().SingleInstance();
            return builder.Build();
        }
    }
}