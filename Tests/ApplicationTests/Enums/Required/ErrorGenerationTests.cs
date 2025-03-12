using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests.Enums.Required;

public class ErrorGenerationTests
{
    [Fact]
    public void Required_ShouldSetCustomMissingError_WhenNoMessageSpecified()
    {
        //Arrange
        var value = Helpers.EnumOneString;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStringParameter(value);
        var property = new EnumProperty<RStringParameter, TestEnum>(parameters, nameStack);
        
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
        var value = Helpers.EnumOneString;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStringParameter(value);
        var property = new EnumProperty<RStringParameter, TestEnum>(parameters, nameStack);
        
        //Act
        
        var requiredProperty = property.Required(Helpers.ExampleMissingErrorMessage);
        
        //Assert
        var missingError = Error.Validation(nameStack.MissingErrorCode, Helpers.ExampleMissingErrorMessage);
        requiredProperty.MissingError.ShouldBe(missingError);
    }
}