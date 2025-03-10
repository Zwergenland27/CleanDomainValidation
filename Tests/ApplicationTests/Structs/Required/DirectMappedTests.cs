using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.Structs.Required;

public class DirectMappedTests
{
    [Fact]
    public void DirectMap_ShouldReturnValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredStructProperty<RStructParameter, int>(parameters, Helpers.ExampleMissingError, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(value);

        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void DirectMap_ShouldReturnDefaultAndSetMissingErrorAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredStructProperty<RStructParameter, int>(parameters, Helpers.ExampleMissingError, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(default);
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
}