Description
=================

OptionType is a small set of classes and interfaces that emulates the functionality provided in F# for the Option type by requiring explicit handling of null values.

Rationale
=================

Mostly because I am lazy and on occasion will write ```FirstOrDefault()``` when using LINQ and then forget to check null. Sometimes null may not be all that exceptional so using ```First()``` doesn't make sense, but then you also need to remeber to handle the null case. This is where the CSharp-OptionType comes in.

Most of implementations of the maybe monad in C# that I have seen look something like this:
```
    SomeOtherType value = Maybe.Create(default(SomeType))
        .With(x => x.AccessSomeProperty)
        .With(y => y.AccessSomePropertysProperty);
```
		
While this implementation is useful in its own right for property chaining, we still have not actually dealt with the possibility of the returned value being null.
		
Some other implementations I have seen work something like this:
```
    Maybe<SomeOtherType> maybe = Maybe.Create(default(SomeType))
        .With(x => x.AccessSomeProperty)
        .With(y => y.AccessSomePropertysProperty);
	  if (maybe.IsSome)
	      DoSome(maybe.Value);
	  else
	      DoNone();
```
It is good that we are returning the actual maybe type with properties for determining if the maybe value is Some/Just or None/Nothing; however, we still has the option of just calling ```maybe.Value``` without checking if the value is actually Some. What is missing from these implementations is an option type that enforces explicit handling of null values. In F#, the assembly will not compile if you have not handled both the Some and None cases when accessing the option value.

This is what handling an option type in F# looks like:
```
    let exists (x : SomeType option) = 
        match x with
            | Some(x) -> true
            | None -> false
```
		
This is what the CSharp-OptionType implementation looks like:
```
    public static bool Exists(Option<SomeType> option) {
        return option
            .Some(x => true)
            .None(false);
    }
```
	
If we were to try to stop at Some with the CSharp-OptionType, our return type would be ```ISomeContext<SomeType, bool>``` and we still woudln't be able to access the result without calling ```None```. This ensures that we will never forget to handle a null value again. It is also serves as an explicit signal to the caller of the method that the return type might be null.

