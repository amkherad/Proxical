using System;
using Microsoft.Extensions.DependencyInjection;

namespace Proxical.DependencyInjection
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddProxical(
            this IServiceCollection serviceCollection,
            Action<ProxicalConfiguration> config
        )
        {
            if (serviceCollection is null) throw new ArgumentNullException(nameof(serviceCollection));
            if (config is null) throw new ArgumentNullException(nameof(config));

            var settings = new ProxicalConfiguration();

            config(settings);

            serviceCollection.AddSingleton(settings);

            serviceCollection.AddScoped<IProxyService>(ProxyServiceFactory);
        }

        public static TService UseProxy<TService>(
            this IServiceProvider sp,
            Action<ProxyContext> config
        )
        {
            var proxyService = sp.GetRequiredService<IProxyService>();

            var context = new ProxyContext();

            config(context);
            
            var result = proxyService.Proxy<TService, TService>(context);

            return result;
        }

        private static IProxyService ProxyServiceFactory(
            IServiceProvider sp
        )
        {
            throw new NotImplementedException();
        }
    }
}