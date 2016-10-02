![fluentOptionals](https://raw.githubusercontent.com/duffleit/fluentOptionals/master/fluentOptionals.png)

# Fluent Optionals
___a lightweight & fluent Option/Optional/Maybe Implementation for .Net & Mono___

[![Build status](https://ci.appveyor.com/api/projects/status/bn58b7k9xeh9073a?svg=true)](https://ci.appveyor.com/project/duffleit/fluentoptionals)
[![Build Status](https://travis-ci.org/duffleit/fluentOptionals.svg)](https://travis-ci.org/duffleit/fluentOptionals)
[![Coverage Status](https://coveralls.io/repos/github/duffleit/fluentOptionals/badge.svg?branch=master)](https://coveralls.io/github/duffleit/fluentOptionals?branch=master)

Usually this is the place to go into detail about Optionals and why you should use them. I won't do this as it is already done better than I ever could: 
* you don't know what's an Optional/Option/Maybe - [read this](https://en.wikipedia.org/wiki/Option_type)
* you know what it is, but you are not sure to use it in your project - [read this](http://programmers.stackexchange.com/a/12836)
* you like to use them in your C# project - [download Fluent Optionals](https://github.com/duffleit/fluentOptionals)

---

## Creating Optionals

Basically there are two types of optinonals, those who __have a value__ (we will call them __Some__-Optionals) and those who __have no value__ (we will call them __None__-Optionals). Creating them is easy:

```csharp
var some = Optional.Some(20);
var none = Optional.None<int>();
```

Through the existence of Null-References in C#, a common use case is to create __Some__ or __None__-Optionals based on a Null-Check. That's why there's a shortcut for this: 

```csharp
//traditional way to create Optionals
var x = (value != null) ? Optional.Some(value) : Optional.None<int>();

//shortcut 1
var y = value.ToOptional(); 

//shortcut 2
var z = Optional.From(value)
```

Those three approaches will always produce the same result. If the given value is _null_ a __None__-Optional is returned, in all other cases a __Some__-Optional.

If _null_ is not the only decision criterion, ``ToOptional()`` or ``Optional.From()`` can be called with a _predicate_. Base on this a __Some__ or __None__-Optional is created. 

```csharp
var value = "david";
var some = value.ToOptional(v => v == "david");
var none = value.ToOptional(v => v == "thomas");

//or
Optional.From(value, v => v == "david");
```

Consider if `ToOptional()` is called on _null_, it will always return a __None__-Optional without even evaluating the predicate (to avoid Null-Reference-Exceptions).

If you want to receive a __Some__ or __None__-Optional explicit, you can use ``value.ToSome()`` or ``ToNone()``:

```csharp
10.ToSome() //always produces a Some-Optional
            //except the value is none, this causes a 'SomeCreationWithNullException'
            
10.ToNone() //ignores the value and always produces a None-Optional
```

## Retrieving values

When retrieving values, Optionals force you to always consider __Some__ and __None__ (if a value is present or not).

``ValueOr()`` is one approach to retrieve an Optional's value. 

```csharp
"Max".ToSome().ValueOr("unknown name"); //returns "Max"

"Max".ToNone().ValueOr("unknown name"); //returns "unknown name"

"Max".ToNone().ValueOr(() => nameServer.GetDefaultName()); //will evaluate value lazy

"Max".ToNone().ValueOrThrow(new Exception("name was not provided")); //throws exception 
```

To verify if an Optional is __Some__ or __None__ the properties ``IsSome`` and ``IsNone`` are provided.

### Match  
Another way to get an Optional's value is to use ``Match()``. Inspired by _pattern matching_ it provides a nice way to handle __Some__ and __None__-Optionals.

```csharp
var optional = "Max".ToSome();

optional.Match(
    name => Console.WriteLine("hey, " + name),
    ()   => Console.WriteLine("we don't know your name")
)
```

``Match`` can also return a value:

```csharp
var optional = "Max".ToSome();

var displayName = optional.Match(
                        name => v.ToUpperCase(),
                        ()   => "unknown PERSON"
                    )
```

Beside ``Match()`` there are provided more specific methods called ``MatchSome()``  and ``MatchNone()``. They only take one action.

## Mapping Optionals

``Map()`` can transform the value of an Optional. If the Optional is __None__ the transformation won't be applied and the new Optional stays __None__. If the give map function returns _null_, the Optional becomes __None__.

```csharp    
10.ToOptional().Map(i => i * 2) //returns Some-Optional<int> -> 20
10.ToNone().Map(i => i * 2) //returns None-Optional<int>
"test".ToOptional().Map(i => null) //returns None-Optional<int> 
```

``Map()`` can also change the Optional's type.

```csharp
Optional<string> result = 10.ToOptional().Map(i => i.ToString()) //returns Some-Optional<String> -> "10"
Optional<string> result = 10.ToNone().Map(i => i.ToString()) //returns None-Optional<String>
Optional<string> result = Optional.None<int>().Map(i => null) //return None-Optional<String>
```

``Shift()`` provides a possibility to change a __Some__ to a __None__-Optional. If ``Shift()`` is called on a __None__-Optional, it does not alter anything. 

```csharp
var transformed = 10.ToOptional().Shift(v => v < 100); //returns None-Optional<int>
var transformed = 200.ToOptional().Shift(v => v < 100); //returns Some-Optional<int> -> 200
```

## Joining Optionals

Joining Optionals can be achieved by using ``Join()``.

```csharp
Optional
    .From(_priceWebService.GetProductPrice(productId))
    .Join(_reductionWebService.GetProductReduction(productId))
    .Join(_taxesWebService.GetProductTaxes(productId, country))
    .Match(
        some: (price, reduction, taxes) => $"{price - reduction} (excl. {taxes}% taxes)",
        none: ()                        => $"price is currently not available"
    )
```

A joined Optional evaluates to none as soon as a __None__-Optional gets joined. 
_Fluent Optionals_ let you join up to 7 Optionals, and beside ``Match()`` also ``MatchSome()``, ``MatchNone()``, ``IsSome`` and ``IsNone`` can be called.

## IEnumerable-Extensions

* ``ToOptionalList()``: transforms a ``IEnumerable`` to a ``IEnumerable`` of Optionals.

    ```csharp
    IEnumerable<Optional<int>> OptionalList = new List<int>{ 1, 2, 3 }.ToOptionalList();
    
    new List<string>{ null, "test", null }.ToOptionalList();
    //returns a List of 3 Optionals: [None, Some, None] 
    
    new List<int>{ 0, 1, 3, 0 }.ToOptionalList(v => v > 0);
    //returns a List of 4 Optionals:  [None, Some, Some, None] 
    ```

* ``FirstOrNone()``: returns the first __Some__-Optional. If there is no first element, it returns a None-Optional..

* ``LastOrNone()``: returns the last __Some__-Optional. If there is no last element, it returns a None-Optional.

* ``SingleOrNone()``: if exactly one __Some__-Optional exists, this is returned. I all other cases it returns a None-Optional.  

# Thanks!

Many thanks to __paulroho__ and __thomaseizinger__ for providing inspiration and feedback.  
And of course to Tony Hoare, if he won't have made his [one billion dollar mistake](http://www.infoq.com/presentations/Null-References-The-Billion-Dollar-Mistake-Tony-Hoare), this library would be absolutely useless. (:
