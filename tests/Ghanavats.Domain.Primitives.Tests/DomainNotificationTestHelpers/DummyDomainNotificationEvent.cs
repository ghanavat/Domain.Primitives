using Ghanavats.Domain.Primitives.DomainEventMechanism;

namespace Ghanavats.Domain.Primitives.Tests.DomainNotificationTestHelpers;

internal class DummyDomainNotificationEvent : DomainNotificationMessageBase
{
    public DummyDomainNotificationEvent()
    {
        NotificationMessage = "Testing notification message";
    }
}
