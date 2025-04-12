using Ghanavats.Domain.Primitives.DomainEventMechanism;
using Microsoft.Extensions.DependencyInjection;

namespace Ghanavats.Domain.Primitives.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddDomainEventPublisher(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();

        return services;
    }
}
