using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Classes.Optional;

public class ComplexMappedTests
{
    #region Class

	[Fact]
    public void ComplexMapClass_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OClassValueObject>(new OClassValueObject(value));
            });


        //Assert
        validatedProperty.ShouldBe(new OClassValueObject(value));
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        validatedProperty.ShouldBeNull();
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
                return new ValidatedOptionalClassProperty<OClassValueObject>((OClassValueObject?)null);
            });

        //Assert
        validatedProperty.ShouldBeNull();
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion
    
    #region Struct
    
    [Fact]
    public void ComplexMapStruct_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(new OStructValueObject(value));
        });

        //Assert
        validatedProperty.ShouldBe(new OStructValueObject(value));
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void ComplexMapStruct_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        validatedProperty.ShouldBeNull();
        
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void ComplexMapStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
            return new ValidatedOptionalClassProperty<OStructValueObject>((OStructValueObject?)null);
        });

        //Assert
        validatedProperty.ShouldBeNull();
        
        property.ValidationResult.HasFailed.ShouldBeFalse();
        
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    #endregion
}