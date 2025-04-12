using Ghanavats.Domain.Primitives.Tests.DomainNotificationTestHelpers;

namespace Ghanavats.Domain.Primitives.Tests.EntityBaseTests;

public class EntityBaseTests
{
    [Fact]
    public void EntityBase_AddDomainEvent_ShouldCorrectlyAddEvent()
    {
        //Arrange
        var dummyEntity = new DummyEntity();
        
        //Act
        dummyEntity.DoSomethingToTriggerAddingEvent();
        
        //Assert
        Assert.NotEmpty(dummyEntity.DomainEvents);
        Assert.Collection(dummyEntity.DomainEvents, domainEventItem =>
        {
            Assert.NotNull(domainEventItem);
            Assert.IsType<DummyDomainNotificationEvent>(domainEventItem);
        });
        
        Assert.Contains(dummyEntity.DomainEvents, item => item.NotificationMessage == "Testing notification message");
        Assert.NotEqual(0, dummyEntity.Id);
    }
    
    [Fact]
    public void EntityBase_Should_CorrectlySetsIdProperty_WhenProtectedConstructorWithIdParameterUsed()
    {
        //Arrange/Act
        var dummyEntity = new DummyEntityOnProtectedParameterisedConstructor(Random.Shared.Next());
        
        //Assert
        Assert.NotEqual(0, dummyEntity.Id);
    }
}
