using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests.Classes;

public record OClassParameter(string? Value) : IParameters;

public record OClassValueObject(string Value)
{
    public static CanFail<OClassValueObject> Create(string value)
    {
        if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
        return new OClassValueObject(value);
    }
}

public record OStructParameter(int? Value) : IParameters;

public record OStructValueObject(int Value)
{
    public static CanFail<OStructValueObject> Create(int value)
    {
        if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
        return new OStructValueObject(value);
    }
}

public class OptionalClassTests
{
    #region Direct Mapped

    [Fact]
    public void DirectMap_ShouldReturnValue_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(value);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void DirectMap_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void DirectMap_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void DirectMap_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion

    #region Factory Mapped

    [Fact]
    public void FactoryMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedValue = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        validatedValue.ShouldBe(new OClassValueObject(value));
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedValue = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        validatedValue.ShouldBe(new OStructValueObject(value));
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }

    [Fact]
    public void FactoryMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }
    
    [Fact]
    public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, OClassValueObject.Create);


        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, OStructValueObject.Create);


        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion

    #region Constructor Mapped

    [Fact]
    public void ConstructorMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
		var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(value);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedValue.ShouldBe(new OClassValueObject(value));
	}

    [Fact]
    public void ConstructorMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
		var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(value);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		validatedValue.ShouldBe(new OStructValueObject(value));
	}

    [Fact]
	public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
		var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(value);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
		var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(value);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
    
    [Fact]
    public void ConstructorMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new OClassValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ConstructorMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new OStructValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ConstructorMapClass_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
		var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ConstructorMapStruct_ShouldReturnNull_WhenValueNull()
    {
		//Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, v => new OStructValueObject(v));

        //Assert
        validatedProperty.ShouldBeNull();
    }

    [Fact]
    public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
		var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
		var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
    
    [Fact]
    public void ConstructorMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new OClassValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ConstructorMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new OStructValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

	#endregion

	#region Complex Mapped

	[Fact]
    public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
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
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
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
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(new OClassValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(new OStructValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(new OClassValueObject(value));
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(new OStructValueObject(value));
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>(Helpers.ExampleValidationError);
            });

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ComplexMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }

    [Fact]
    public void ComplexMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }
    
    [Fact]
    public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OClassValueObject>((OClassValueObject?)null);
            });

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>((OStructValueObject?)null);
            });

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OClassValueObject>((OClassValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>((OStructValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>((OClassValueObject?)null);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>((OStructValueObject?)null);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion
}