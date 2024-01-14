# CleanDomainValidation
With this package you write your validation logic once in your domain layer and use it anywhere by just calling one method.
By returning custom error objects and not throwing exceptions you can achieve a better information flow without try catch blocks all over your code!
This package also brings methods for better validation and mapping of API Requests to commands or queries using an validator optimized for error handling.
The CanFail part of the package is inspired by the [ErrorOr Package](https://github.com/amantinband/error-or/)

## How Domain validation works
Any Entity parameter that needs to be validated must be implemented as a new class. This is called a "value object" and you will get many more benefits with this pattern.
Instead of using a public constructor, make it private and create a static factory method for creating an object instance. Put your validation logic in this method and make it
return CanFail<T>.

### Example
If you are creating a user entity with an Email Field, create a valueobject for the email type and use it instead of string.
```cs
public class Email
{
  private Email(string value)
  {
    Value = value;
  }

  public string Value {get; private set;}

  public CanFail<Email> Create(string value)
  {
    if(String.IsNullOrEmpty(value)
    {
      return Error.Validation("Email.Empty", "The email cannot be empty");
    }

    if(!value.Contains('@'))
    {
      return Error.Validation("Email.Invalid", "The email is in an invalid format");
    }

    return new Email(value);
  }
}
```
You can create the Email with the following code:
```cs
public CanFail CreateUser(string email)
{
  //Call the factory method
  var emailCreationResult = Email.Create(email);

  //check if email creation failed
  if(emailCreationResult.HasFailed)
  {
    //return errors
    return CanFail.FromFailure(emailCreationResult);
  }
  
  //create user with the result value
  User user = new User(emailCreationResult.Value);

  //return success
  return CanFail.Success();
}
```
But feel free of using the Error class and CanFail for other methods that have to handle exceptions as well.

# The more important part: validating API requests
The most annoying part of implementing a REST API with clean architecture and DDD is redefining validation behaviour in the Application Layer and not receiving the validated valueobjects.
Since we already created the factory method which supports error handling, there is no more need for custom validation configurations.

If you are familiar with the CQRS pattern and MediatR, you are used to the following behaviour:
- The json request is automatically mapped to an object containing the needed parameters
- A manually configured validator will validate the incoming object paramers and reject the request if the validation fails
- A request object needs to be created
- The request object will be sent to the request handler
- Use case will be executed
The bad information first: You will need all of this steps for this approach as well BUT:
- The request object contains already the value objects (so they can be used much easier in the handler)
- The factory methods of the value objects can be used for validation, so no custom validation logic needs to be implemented
- If something fails, CanFail will be used to return the errors so they can be handled in the controller without Exception handling

Lets think about a REST API where you can create a user. You have an endpoint that takes a user json of the following format:
```json
{
    "email": "meow"
}
```

The json will be converted to
```cs
public class UserParameter : IParameter
{
    public string? Email {get; set;}
}
```
ℹ️ All parameters will be defined as nullable as we want to handle null values manually as well.
Our command will be looking like this:
```cs
public record CreateUserCommand(Email Email) : ICommand
```
With those two clases we can create the validator for the command:
```cs
public class CreateCommandValidator : CommandValidator<UserParameter, CreateUserCommand>>
{
    protected override void Configure(UserParameter parameters)
    {
        //Configure the email as an required parameter
        //so the given Error wil be returned when it is null. 
        var validatedEmail = RequiredAttribute(
            parameters.Email, Error.Validation("User.Email.Missing", "The email attribute is missing",
            value => Email.Create(value));

        //Configure the method that will be called to create the command
        CreateInstance(() => new CreateUserCommand(validatedEmail));
    }
}
```

In the API Controller, the command can be created the following way:
```cs
public void ControllerMethod(UserParameter parameters)
{
    var createUserCommand = CommandBuilder<CreateUserCommand>
        .AddParameter(parameters)
        .ValiateByUsing<CreateCommandValidator>();

    //Handle command creation failure
    if(createUserCommand.HasFailed)
    {
        ...
        return;
    }
    
    //You can access the valid command here using createUserCommand.Value
}
```

To Be Continued...
