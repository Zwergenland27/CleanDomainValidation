using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Required;

public class ErrorGenerationTests
{
    [Fact]
    public void Required_ShouldSetCustomMissingError_WhenNoMessageSpecified()
    {
        //Arrange
        List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassListParameter(value);
        var property = new ListProperty<RClassListParameter, string>(parameters, nameStack);
        
        //Act
        
        var requiredProperty = property.Required();
        
        //Assert
        var missingError = Error.Validation(nameStack.MissingErrorCode, nameStack.MissingErrorMessage);
        requiredProperty.MissingError.ShouldBe(missingError);
    }
    
    [Fact]
    public void Required_ShouldSetCustomMissingErrorWithCustomMessage_WhenMessageSpecified()
    {
        //Arrange
        List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassListParameter(value);
        var property = new ListProperty<RClassListParameter, string>(parameters, nameStack);

        //Act
        
        var requiredProperty = property.Required(Helpers.ExampleMissingErrorMessage);
        
        //Assert
        var missingError = Error.Validation(nameStack.MissingErrorCode, Helpers.ExampleMissingErrorMessage);
        requiredProperty.MissingError.ShouldBe(missingError);
    }
}