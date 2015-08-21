---
layout: default
title: Abstractions
---

# [Abstractions](http://en.wikipedia.org/wiki/Abstraction_&#40;computer_science&#41;)

[CQRS](https://msdn.microsoft.com/en-us/library/jj554200.aspx).

----------

## [Commands](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=91) 

> Actions that change something and return nothing

```csharp
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    void Execute(TCommand command);
}
```

----------

## [Queries](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=92)

> Actions that return something and change nothing

```csharp
public interface IQuery<TResult>
{
}

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    TResult Execute(TQuery query);
}
```

----------

## [Mediators](http://en.wikipedia.org/wiki/Mediator_pattern)

> A combination of commands and queries

There are two mediator abstractions. One that essentially mediates a call to a command and returns nothing

```csharp
public interface IMediator
{
}

public interface IMediatingHandler<TRequest> where TRequest : IMediator
{
    void Execute(TRequest request);
}
```

And one that mediates the call to a query and returns something

```csharp
public interface IMediator<TResult> : IMediator
{
}

public interface IMediatingHandler<TRequest, TResult> where TRequest : IMediator<TResult>
{
    TResult Execute(TRequest request);
}
```

----------

# Events ([Observers](http://www.dofactory.com/net/observer-design-pattern))

> Side effects

```csharp
public interface IEvent
{
}

public interface ISubscriber<TEvent> where TEvent : IEvent
{
    void Handle(TEvent param);
}
```

The system has 3 events to cater for _pre_ and _post_ an activity: events are triggered before and after calling each `QueryHandler<,>` and `CommandHandler<>`.

```csharp
public sealed class OnBefore<TRequest> : IEvent
{
    public OnBefore(TRequest response)
    {
        this.Request = response;
    }

    public TRequest Request { get; private set; }
}

public sealed class OnAfter<TRequest> : IEvent
{
    public OnAfter(TRequest request)
    {
        this.Request = request;
    }

    public TRequest Request { get; private set; }
}

public sealed class OnAfter<TRequest, TResponse> : IEvent
{
    public OnAfter(TRequest request, TResponse response)
    {
        this.Request = request;

        this.Response = response;
    }

    public TRequest Request { get; private set; }

    public TResponse Response { get; private set; }
}
```

----------

## [Decorators](http://programmers.stackexchange.com/a/139113)

> Cross cutting concerns

The are a number of decorators within the application. These decorators can be logically divided into 2 groups.

#### Specific cross cutting concerns ([A Strategy](http://en.wikipedia.org/wiki/Strategy_pattern))

Decorators that affect only one thing. A great example is the **deny local print** feature. Rather than open up the existing fully unit tested code that queries EngageOne for delivery options (in doing so we would break the _S_ and the _O_ in the [SOLID](http://en.wikipedia.org/wiki/SOLID_%28object-oriented_design%29) principles) we wrap that query with a decorator.

#### General cross cutting concerns ([Aspect Oriented Programming](http://en.wikipedia.org/wiki/Aspect-oriented_programming))

Code that should be applied to all (or the majority of) a specific service type. User activity logging is an example of this. All commands (and queries) are wrapped with a generic decorator that records certain details on the requested action.
