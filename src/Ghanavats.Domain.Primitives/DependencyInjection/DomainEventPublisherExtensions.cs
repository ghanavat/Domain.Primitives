using Ghanavats.Domain.Primitives.DomainEventMechanism;
using Microsoft.Extensions.DependencyInjection;

namespace Ghanavats.Domain.Primitives.DependencyInjection;

public static class DomainEventPublisherExtensions
{
    public static IServiceCollection AddDomainEventPublisher(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();

        return services;
    }
}
