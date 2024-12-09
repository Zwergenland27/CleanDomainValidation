# CleanDomainValidation
Domain Model Validation and Rich Domain Models with Value Objects are one of the most important parts of DDD. But as soon as you need
to implement those in the domain and application layer, you will soon or later be confused about the implementation details, especially in the request mapping and validation stage.
By using this package, you will write your validation logic once in the domain layer and will be able to use it more easily in the application layer.

## How Domain validation works
Instead of throwing Exceptions all over your code, result objects will be used to indicate that a method can fail. The idea for this result object
is originally from the [ErrorOr Package](https://github.com/amantinband/error-or/).

### How Errors work

To indicate that something went wrong, `Error` classes are used. Since most backends are accessed through the web via HTTP, the Error objects
are seperated into different groups conforming to [RFC 7231](https://datatracker.ietf.org/doc/html/rfc7231#section-6):
- Conflict: The request cannot be processed because the domain logic does not allow this action. Example: A user is trying to change his birthday to the future
- Not found: The requested object cannot not be found
- Validation: The request contains invalid objects, for example an invalid email
- Forbidden: The user is not authorized to do what he did, for example when a non admin user is trying to access the admin console
When an error is generated, an error code and a message needs to be specified.
- Error code: Per project unique code that explains the error and its source with one word. I suggest the following format: AggregateRoot[.SubEntity].ErrorCode
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

Using Errors in your code can be done in two ways:
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
  return CanFail.Success;
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

Error handling works exactly like [error handling for void returning methods](#void-returning-methods) but instead of returning `CanFail.Success`, `T` can be returned.

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

    return CanFail.Success;
  }

  private static CanFail CheckForbidden(string value)
  {
    if(value == "Admin") return Errors.Aggregate.Username.Invalid;
    return CanFail.Success;
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

## How Application layer validation works
Now to the most important part of this package: reusing the validation logic defined in the domain layer for validating user requests. Since validation and request mapping are closely coupled, you can implement both logics in one class by using request builders. No more need to have a seperate mapping and validation layer!

### Prerequisites
Before implementing request builders, you need to define your parameter and request objects.
- Parameter objects contain all parameters that the user have to pass into the program using primitive types (like int, string, etc.)
- Parameter objects must implement `IParameters` interface
- Request objects contain only (then validated) objects that are also used in the domain language for processing a specific request
- Request objects must implement `IRequest` interface

> **Example:** <br>
> Creating a user identified by username, age and usertype.
>
> Following classes are defined in the domain layer:
>```cs
>//User we want to create
>public class User {
>
>  //Strongly typed parameter (ValueObject)
>  public Username Username { get; private set; }
>
>  //Strongly typed parameter (ValueObject)
>  public Age Age { get; private set; }
>
>  //Enum that can be null
>  public UserType? Type { get; private set; }
>
>  /*
>  Constructor
>  */
>
>  //Factory method ontaining validation logic that can fail
>  public static CanFail<User> Create(Username username, Age age, UserType? type){
>    /*
>    validation logic
>    */
>  }
>}
>```
> The following classes would be needed for working with the user input:
>```cs
>//This is the class containing the parameters that the user will need fill out
>//How the object is created depends on the users needs
>public class CreateUserParameters : IParameters
>{
>
>  //User can insert any text, this must be validated for the Username value object!
>  public string? Username { get; set; }
>
>  //Since we don't now if the user will actually fill out the parameter, we make them nullable even if they are required
>  public int? Age { get; set; }
>
>  //Enums can be mapped using strings or integers
>  public string? UserType {get; set;}
>}
>
>//Request object that contains all validated and converted parameters of the request
>public record CreateUserRequest(
>  Username Username,
>  Age Age,
>  UserType? Type) : IRequest; 
>```
>

### Mapping and validating using request builder
To map and validate the parameter object `TParameters` to the given request object `TRequest`, a request builder class is required which implements `IRequestBuilder<TParameters, TRequest>`. The magic will happen in the `Configure` method with the help of the builder passed to the method.
The builder is used to configure each property of `TRequest` and populate it from a given property of `TParameters` using a fluent API. The scheme of defining the property is always the same:

#### Define the type of the property
Since mapping is done in a strongly typed way, you need to tell the builder what type the property of the request object has. Currently supported types are:
Type | Function |
--- | --- |
Classes | builder.ClassProperty(property) |
Structs | builder.StructProperty(property) |
Enums | builder.EnumProperty(property) |
Lists | builder.ListProperty(property) |

You can select the fitting type using the functions from the second column. To select which property you want to map, you have to pass a lambda expression in the property parameter which selects the property of the request. After defining the mapping, the function will return the validated property into a variable that is later used to create the request object. So we can already assign the result to a variable.

> Continuation of previous example:
> ```cs
> //Maps the Username property of the request object and store the validated result in the variable validatedUsername
> Username validatedUsername = builder.ClassProperty(request => request.Username)
> /*
> ...
> */
> //You can safely use the var keyword instead of typing the class name
> var validatedAge = builder.ClassProperty(request => request.Age)
> /*
> ...
> */
> UserType? validatedType = builder.EnumProperty(request => request.Type)
> /*
> ...
> */
> ```

#### Define if property should be optional, required or default
In the next step we need to define if the property should be optional or required. This setting needs to match up with the nullability defined in the request object as it will determine the return type of the builder function. Since we want a warning if the user does not enter a required field, an error needs to be passed to the method.
You can also set a default value for a required field that will be used when the parameter is not set in the request paramters.

> Continuation of previous example:
> ```cs
> //Maps the Username property of the request object and store the validated result in the variable validatedUsername
> Username validatedUsername = builder.ClassProperty(request => request.Username)
>                                 .Required(Error.Validation("Request.CreateUser.Username.Missing", "The username field is missing."))
> /*
> ...
> */
> //You can safely use the var keyword instead of typing the class name
> var validatedAge = builder.ClassProperty(request => request.Age)
>                                 .WithDefault(new Age(18))
> /*
> ...
> */
> UserType? validatedType = builder.EnumProperty(request => request.Type)
>                                 .Optional()
> /*
> ...
> */
> ```

#### Map the request parameter
In the next step we can finally map the request parameters from the `TParameters` object. To do so, we need to call one of the various .Map(...) methods of the fluent api.

##### Direct mapping
Description: The most simple way: Map a property which is the same type in the request and the parameter like `string` and `int`

Usage:
`.Map(parameterProperty)`

> Example of mapping a string property (Description)
>```cs
>
>//Maps the Username property of the request object and store the validated result in the variable validatedDescription
>Username validatedDescription = builder.ClassProperty(request => request.Description)
>                                .Required(Error.Validation("Request.CreateUser.Username.Missing", "The username field is missing."))
>                                .Map(parameters => parameters.Description)
>```

##### Constructor mapping
Description: Map property of type `ClassName` that can be constructed from a simple type like `string` or `int` to an object by using its constructor

Usage:
`.Map(parameterProperty, constructorLambda)`
> Example of mapping a string property (Description) to a `Description` object
> ```cs
> record Description(string Value);
> //Maps the Description property of the request object and store the validated result in the variable validatedDescription
> Description validatedDescription = builder.ClassProperty(request => request.Description)
>                                    .Required(Error.Validation("Request.CreateUser.Description.Missing", "The description field is missing."))
>                                    .Map(parameters => parameters.Description, descriptionValue => new Description(parameters.Description))
> ```
##### Factory mapping
Description: Map property of type `ClassName` that can be constructed from a simple type like `string` or `int` to an object by using its factory method that can fail

Usage:
`.Map(parameterProperty, factoryLambda)`
> Example of mapping a string property (Description) to a `Description` object
> ```cs
> class Description
> {
>   private Description(string value)
>   {
>     Value = value;
>   }
>
>   public string Value {get;}
>
>   public static CanFail<Description> Create(string value)
>   {
>     if(value.Length < 5) return Error.Validation("Request.CreateUser.Description.TooShort", "The description is too short");
>     if(value.Length > 100) return Error.Validation("Request.CreateUser.Description.TooLong", "The description is too long");
>
>     return new Description(value);
>   }
> }
>
> //Maps the Description property of the request object and store the validated result in the variable validatedDescription
> Description validatedDescription = builder.ClassProperty(request => request.Description)
>                                    .Required(Error.Validation("Request.CreateUser.Description.Missing", "The description field is missing."))
>                                    .Map(parameters => parameters.Description, value => Description.Create(value))
> //Short version
> Description validatedDescription = builder.ClassProperty(request => request.Description)
>                                   .Required(Error.Validation("Request.CreateUser.Description.Missing", "The description field is missing."))
>                                   .Map(parameters => parameters.Description, Description.Create)
> ```

##### Complex mapping
Description: Map property of type `ClassName` that can only be constructed from multiple simple types like `string` or `int` of the parameter object
> [!TIP]
> Since the builder works the same as the request builder, you should first read chapter [Return the request object](#return-the-request-object) to understand how to return the request object

Usage:
As a second parameter, a lambda expression for a builder is passed that treats the complex property as a new request object that can be built upon multiple parameters 
`.Map(nestedParameterProperty, propertyBuilder)`

> Example of mapping a string and int property (Description) to a `Description` object
> ```cs
> record Description(string Value, int Length);
> //Maps the Description property of the request object and store the validated result in the variable validatedDescription
> Description validatedDescription = builder.ClassProperty(request => request.Description)
>                                   .Required(Error.Validation("Request.CreateUser.Description.Missing", "The description field is missing."))
>                                   .Map(parameters => parameters.Description, builder => {
>                                       var validatedValue = builder.ClassProperty(request => request.Value)
>                                           .Required(Error.Validation("Request.CreateUser.Description.Value.Missing", "The value field is missing."))
>                                           .Map(parameters => parameters.Value);
>                                       var validatedLength = builder.StructProperty(request => request.Length)
>                                           .Required(Error.Validation("Request.CreateUser.Description.Length.Missing", "The length field is missing."))
>                                           .Map(parameters => parameters.Length);
>                                     return new Description(validatedValue, validatedLength);
>                                  });
> ```

#### Enum mapping
Description: Map enum property from an `string` or `int`

Usage:
`.Map(enumParameterProperty, invalidEnumError)`

> Example of mapping a string property (Type) to a `UserType` enum
> ```cs
> //Maps the Type property of the request object and store the validated result in the variable validatedType
> UserType? validatedType = builder.EnumProperty(request => request.Type)
>                          .Optional()
>                          .Map(parameters => parameters.Type, Error.Validation("Request.CreateUser.Type.Invalid", "The type field is invalid."));
> ```

#### Return the request object
After all properties have been configured and validated by using the builder, the request object
can be created by returning the `builder.Build(buildMethod)` method of the builder and passing the build method for the request object as a parameter.

> Example of returning the request object
> ```cs
> //Maps the Username property of the request object and store the validated result in the variable validatedUsername
> Username validatedUsername = ...
> //You can safely use the var keyword instead of typing the class name
> var validatedAge = ...
> UserType? validatedType = ...
> 
> //Return the request object
> return builder.Build(() => new CreateUserRequest(validatedUsername, validatedAge, validatedType));
> 
> //This of course works with factory methods that can fail as well
> return builder.Build(() => CreateUserRequest.Create(validatedUsername, validatedAge, validatedType));
> ```

The builder will only call the build method if all properties have been validated successfully. If an error occurs, the builder will return the error object automatically.
Since the Map methods are all strongly typed, you will get compiler warnings when you try to pass a nullable value to a required parameter in the factory method / constructor.