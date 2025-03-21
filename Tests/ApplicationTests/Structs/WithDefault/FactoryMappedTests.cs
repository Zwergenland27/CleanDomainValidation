using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;
using Tests.ApplicationTests.Structs.Required;

namespace Tests.ApplicationTests.Structs.WithDefault;

public class FactoryMappedTests
{
    #region Class

    [Fact]
    public void FactoryMapClass_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedValue = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        validatedValue.ShouldBe(new RClassValueObject(value));
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnDefaultAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        validatedProperty.ShouldBe(default);
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);


        //Assert
        validatedProperty.ShouldBe(defaultValue);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion
    
    #region Struct
    
    [Fact]
    public void FactoryMapStruct_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedValue = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        validatedValue.ShouldBe(new RStructValueObject(value));
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void FactoryMapStruct_ShouldReturnDefaultAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        validatedProperty.ShouldBe(default);
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void FactoryMapStruct_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);


        //Assert
        validatedProperty.ShouldBe(defaultValue);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    #endregion
}