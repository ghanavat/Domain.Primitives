# Ghanavats.Domain.Primitives

A lightweight, opinionated foundation for building rich domain models in Domain-Driven Design (DDD).

## Overview
This package provides essential building blocks for creating expressive and consistent domain layers, 
including support for Value Objects, Entities, Aggregate Root tagging, and domain event dispatching.

## ✨ Features
✅ EntityBase with identity and equality support

✅ ValueObject base class with built-in equality and immutability handling

✅ AggregateRoot attribute to clearly tag aggregate roots

✅ Domain event infrastructure to publish and handle domain events

## 📦 Installation

Install via NuGet:
```shell
dotnet add package Ghanavats.Domain.Primitives
```

## 🧱 Core Components
### EntityBase
Base class for domain entities with identity-based equality comparison.

### ValueObject
Base class for creating immutable value objects with structural equality. Example:

```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
```

### [AggregateRoot] Attribute
Instead of forcing an IAggregateRoot interface, 
this attribute provides a clean way to tag aggregate roots.

```csharp
[AggregateRoot]
public class Order : EntityBase
{
    // Domain logic...
}
```

#### 🧩 What's the issue with empty marker interfaces?
In traditional DDD, it’s common to define an interface like this:

```csharp
public interface IAggregateRoot { }
```
And then have your aggregates implement it:
```csharp
public class Order : EntityBase, IAggregateRoot
{
    // Domain logic
}
```
This interface has no methods or properties — it’s purely used as a marker.
However, analysers (like from Roslyn or tools like ReSharper and StyleCop) 
often raise warnings about this pattern:

> IDE0067 / CA1040: "Do not declare empty interfaces."

This is because:
* Empty interfaces don’t convey behaviour, which is the primary purpose of an interface in OOP.
* They're hard to reflect on meaningfully in tooling and might cause confusion in large codebases.
* They are often better replaced with attributes, which are purpose-built for adding metadata.

#### ✅ Why [AggregateRoot] is better
By switching to a custom attribute, you're using a more semantically correct and analyser-friendly 
approach for metadata tagging.

```csharp
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class AggregateRootAttribute : Attribute
{
}
```

#### 🛠️ Use-case for [AggregateRoot]
If you’re doing something like domain scanning 
(e.g. identifying aggregate roots at startup or validation), 
you can reflect on this attribute easily:

```csharp
var aggregateRootTypes = AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(a => a.GetTypes())
    .Where(t => t.GetCustomAttribute<AggregateRootAttribute>() != null)
    .ToList();
```

This can be useful in:
* Domain model validation
* Automatic registration
* Code generation / source generators
* Documentation tools

### Domain Event Dispatching
Built-in support to create, add, and consume domain events from aggregates.

```csharp
public class User : EntityBase
{
    public string Email { get; private set; }

    public User(int id, string email)
        : base(id)
    {
        Email = email;
        AddDomainEvent(new UserCreatedDomainEvent(id, email));
    }
}
```

## ✅ When to Use
Use this package in your domain layer if you:

* Want a clean, reusable foundation for DDD modelling
* Need consistent equality and identity rules
* Prefer using attributes for marking aggregate roots
* Want built-in support for collecting and dispatching domain events
