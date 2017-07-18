using System;
using Autofac;

namespace boss.client.win
{
    public static class AutofacExtension
    {
        public static Page ResolvePage(this IComponentContext container, string code, object parameter)
        {
            var result = container.ResolveOptionalNamed<Page>(GetPageFullName(code), new NamedParameter("parameter", parameter));
            if (result == null) throw new ApplicationError("message.unimplemented-page-resolved", code);
            return result;
        }

        public static void RegisterAllTypes(this ContainerBuilder builder)
        {
            RegisterAllServices(builder);
            RegisterAllPages(builder);
        }

        private static void RegisterAllServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0)
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();
        }

        private static void RegisterAllPages(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetCustomAttributes(typeof(PageAttribute), false).Length > 0)
                .Named<Page>(x => GetPageFullName(((PageAttribute)x.GetCustomAttributes(typeof(PageAttribute), false)[0]).Code))
                .InstancePerDependency();
        }

        private static string GetPagePrefix()
        {
            return "PAGE.";
        }

        private static string GetPageFullName(string code)
        {
            return GetPagePrefix() + code;
        }
    }
}