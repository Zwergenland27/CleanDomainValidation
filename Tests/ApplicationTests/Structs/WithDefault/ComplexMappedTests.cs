using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;
using Tests.ApplicationTests.Structs.Required;

namespace Tests.ApplicationTests.Structs.WithDefault;

public class ComplexMappedTests
{
     #region Class

	[Fact]
    public void ComplexMapClass_ShouldPassNameStackWithPropertyNameToBuilderAndReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                //Assert
                builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
                return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
            });


        //Assert
        validatedProperty.ShouldBe(new RClassValueObject(value));
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldPassNameStackWithPropertyNameToBuilderAndReturnDefaultAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            //Assert
            builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
            return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        validatedProperty.ShouldBe(default);
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
                return new ValidatedRequiredProperty<RClassValueObject>((RClassValueObject?)null);
            });

        //Assert
        validatedProperty.ShouldBe(defaultValue);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion
    
    #region Struct
    
    [Fact]
    public void ComplexMapStruct_ShouldPassNameStackWithPropertyNameToBuilderAndReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            //Assert
            builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
            return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
        });

        //Assert
        validatedProperty.ShouldBe(new RStructValueObject(value));
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void ComplexMapStruct_ShouldPassNameStackWithPropertyNameToBuilderAndReturnDefaultAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            //Assert
            builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
            return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        validatedProperty.ShouldBe(default);
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void ComplexMapStruct_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
            return new ValidatedRequiredProperty<RStructValueObject>((RStructValueObject?)null);
        });

        //Assert
        validatedProperty.ShouldBe(defaultValue);
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    #endregion
}