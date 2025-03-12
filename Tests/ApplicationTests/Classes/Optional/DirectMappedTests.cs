using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Classes.Optional;

public class DirectMappedTests
{
    [Fact]
    public void DirectMap_ShouldReturnValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(value);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void DirectMap_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBeNull();
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
}