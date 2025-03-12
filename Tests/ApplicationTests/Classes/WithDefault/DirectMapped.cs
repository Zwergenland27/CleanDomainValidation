using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using Shouldly;
using Tests.ApplicationTests.Classes.Required;

namespace Tests.ApplicationTests.Classes.WithDefault;

public class DirectMapped
{
    [Fact]
    public void DirectMap_ShouldReturnValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var value = Helpers.ExampleStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(value);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void DirectMap_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(defaultValue);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
}