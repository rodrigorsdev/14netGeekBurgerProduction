using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Extensions.Configuration;

namespace GeekBurger.Production
{
    /// <summary>
    /// Service bus extensions methods
    /// </summary>
    public static class ServiceBusNamespaceExtension
    {
        /// <summary>
        /// Get the service bus namespace
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceBusNamespace GetServiceBusNamespace(this IConfiguration configuration)
        {
            var config = configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();

            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(config.ClientId, config.ClientSecret, config.TenantId, AzureEnvironment.AzureGlobalCloud);

            var serviceBusManager = ServiceBusManager.Authenticate(credentials, config.SubscriptionId);
            return serviceBusManager.Namespaces.GetByResourceGroup(config.ResourceGroup, config.NamespaceName);
        }
    }
}
