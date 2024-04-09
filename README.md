# CleanDomainValidation
Domain Model Validation and Rich Domain Models with Value Objects are one of the most important parts of DDD. But as soon as you need
to implement those in the domain and application layer, that this is not really a nice part, especially in the request mapping and validation stage.
By using this package, you will write your validation logic once in the domain layer and will be able to use it more easily in the application layer.

## How Domain validation works
Instead of throwing Exceptions all over your code, result objects will be used to indicate that a method can fail. The idea for this result object
is originally from the [ErrorOr Package](https://github.com/amantinband/error-or/).

### How Errors work

To indicate that something went wrong, `Error` classes are used. Since most backends are accessed through the web via HTTP, the Error objects
are seperated into different groups conforming to [RFC 7231](https://datatracker.ietf.org/doc/html/rfc7231#section-6):
- Conflict: The request cannot be processed because the domain does not allow it. Example: A user is trying to change his birthday to the future
- Not found: The requested object cannot not be found
- Validation: The request contains invalid objects, for example an invalid email
- Forbidden: The user is not authorized to do what he did, for example when a non admin user is trying to access the admin console
When an error is generated, an error code and a message needs to be specified.
- Error code: Unique human readable code that explains the error with one word. I suggest the following format: AggregateRoot[.SubEntity].ErrorCode
- Message: Human readable message that explains what went wrong more detailed

> **Example:** <br>
> A company aggregate contains employee entities, where the birthday can be set
> The birthday of the employee needs to be in the past and must be at least 18 years ago.
> The errors might look like the following:
> ```cs
> //Birthday is in the future error
> Error.Conflict("Company.Employee.BirthdayInTheFuture", "The birthday of a person cannot be in the future");
>
> //Employee is younger then 18 error
> Error.Conflict("Company.Employee.TooYoung", $"The employee needs to be 18 years old, but is only {age} years old.")
> ```

> [!TIP]
> To achieve a better overview of all the domain errors that can occur, it may be useful to create a static Error class for each aggregate.
Example Errors.Company.cs:
>```cs
>public static partial class Errors
>{
>  public static class Company
>  {
>    public static Error NotFound => Error.NotFound(
>      "Company.NotFound",
>      "The company with this id could not be found");
>
>    public static Error Employee
>    {
>      public static Error BirthdayInTheFuture => Error.Conflict(
>        "Company.Employee.BirthdayInTheFuture",
>        "The birthday of a person cannot be in the future");
>      
>      public static Error TooYoung(int age) => Error.Conflict(
>        "Company.Employee.TooYoung",
>        $"The employee needs to be 18 years old, but is only {age} years old.");
>    }
>  }
>}
>```
> You now have a specification of all errors of the domain in one place which makes it much more easy to maintain them.

### How result objects work
#### Void returning methods
A void returning method that contains code that can fail, should use `CanFail` as a return type.

Using Errrors in your code can be done in two ways:
```cs
//Option 1: return Error object
public CanFail CanFailByReturn(int number)
{
  //Error object will be implicitly converted to CanFail with this error
  if(number < 5) return Error.Validation("Aggregate.NumberTooSmall", "The number cannot be smaller than 5");

  //Works the same as in the example above, but uses the static Error class which makes the code much more readable and descriptive
  if(number > 10) return Errors.Aggregate.NumberTooBig;

  this.Number = number;

  //Marks that the method has been executed successfully
  return CanFail.Success();
}

//Option2: return CanFail object
public CanFail CanFailByObject(int number){
  
  //new CanFail instance has not failed
  CanFail result = new CanFail();

  if(number < 5)
  {
    result.Failed(Errors.Aggregate.NumberTooSmall);
  }
  else if(number > 10)
  {
    result.Failed(Errors.Aggregate.NumberTooBig);
  }

  //If the result has not failed
  if(!result.HasFailed)
  {
      //Set the validated number
      this.Number = number;
  }

  //Return CanFail object which contains occured errors
  return result;
}
```

#### `T` returning methods
A `T` returning method that contains code that can fail, should use `CanFail<T>` as a return type

Error handling works exactly like [error handling for void returning methods](#void-returning-methods) but instead of returning `CanFail.Success()`, `T` can be returned.

> Example for an Email [value object](https://martinfowler.com/bliki/ValueObject.html) with the factory method `Create` 
>```cs
>public class Email
>{
>  //Private, so that the class can only be created from the factory method
>  //which validates the value
>  private Email(string value)
>  {
>    Value = value;
>  }
>
>  public string Value {get;}
>
>  public static CanFail<Email> Create(string value)
>  {
>    //The IsEmail() method will check if the email is valid
>    if(!value.IsValidEmail()) return Errors.Aggregate.Email.Invalid;
>
>    return new Email(value);
>  }
>  
>  //It is also possible to use the result object
>  public static CanFail<Email> AlternativeCreate(string value)
>  {
>    CanFail<Email> result = new();
>    if(!value.IsValidEmail())
>    {
>      result.Failed(Errors.Aggregate.Email.Invalid)
>    }
>    else
>    {
>      result.Success(new Email(value));
>    }
>    return result;
>  }
>}
>```

#### Nested Failures
In many cases the error is not directly generated in the outer method itself but the method is calling an inner method that returns a result object.
Obviously if the inner method can fail, the outer method must return a result object as well.
The following examples will show how to forward inner errors.
```cs
public class Username
{
  private Username(string value)
  {
    Value = value;
  }

  public string Value {get;}

  private static CanFail Validate(string value)
  {
    if(value.Length < 3) return Errors.Aggregate.Username.TooShort; 
    if(value.Length > 10) return Errors.Aggregate.Username.TooLong;

    return CanFail.Success();
  }

  private static CanFail CheckForbidden(string value)
  {
    if(value == "Admin") return Errors.Aggregate.Username.Invalid;
    return CanFail.Success();
  }

  public static CanFail<Username> Create(string value)
  {
    var validationResult = Validate(value);

    //Forwarding the errors that occured in validationResult
    if(validationResult.HasFailed) return validationResult.Errors;

    return new Email(value);
  }

  //In this example multiple checks will be stored in the result
  public static CanFail<Username> CreateWithForbiddenCheck(string value)
  {
    CanFail<Username> result = new();

    var validationResult = Validate(value);
    result.InheritFailure(validationResult);
    
    var forbiddenResult = CheckForbidden(value);
    result.InheritFailure(forbiddenResult);

    if(!result.HasFailed)
    {
      result.Success(new Email(value));
    }

    return result;
  }
}
```