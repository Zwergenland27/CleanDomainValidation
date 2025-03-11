using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;
using Tests.ApplicationTests.Structs.Required;

namespace Tests.ApplicationTests.Structs.WithDefault;

public class DirectMappedTests
{
    [Fact]
    public void DirectMap_ShouldReturnValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultIntValue;
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, int>(parameters, defaultValue, nameStack);

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
        var defaultValue = Helpers.DefaultIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, int>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(defaultValue);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
}