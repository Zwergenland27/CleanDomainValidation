using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Classes;

public class RequiredClassWithDefaultTests
{
    #region Direct Mapped

    [Fact]
    public void DirectMap_ShouldReturnValue_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(value);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void DirectMap_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void DirectMap_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void DirectMap_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = Helpers.DefaultStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue, nameStack);

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
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedValue = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        validatedValue.ShouldBe(new RClassValueObject(value));
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedValue = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        validatedValue.ShouldBe(new RStructValueObject(value));
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }

    [Fact]
    public void FactoryMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }
    
    [Fact]
    public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);


        //Assert
        validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);


        //Assert
        validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion
    
    #region Constructor Mapped
    [Fact]
    public void ConstructorMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedValue.ShouldBe(new RClassValueObject(value));
	}

    [Fact]
    public void ConstructorMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedValue.ShouldBe(new RStructValueObject(value));
	}

    [Fact]
	public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
    
    [Fact]
    public void ConstructorMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new RClassValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ConstructorMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new RStructValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ConstructorMapClass_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(null);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void ConstructorMapStruct_ShouldReturnDefaultValue_WhenValueNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.Map(p => p.Value, v => new RStructValueObject(v));

        //Assert
        validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(null);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(null);
		var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
    
    [Fact]
    public void ConstructorMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new RClassValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ConstructorMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.Map(p => p.Value, v => new RStructValueObject(v));

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

	#endregion
    
    #region Complex Mapped

	[Fact]
    public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
            });


        //Assert
        validatedProperty.ShouldBe(new RClassValueObject(value));
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
            });

        //Assert
        validatedProperty.ShouldBe(new RStructValueObject(value));
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ExampleStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ExampleIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
            });

        //Assert
        validatedProperty.ShouldBe(null);
    }

    [Fact]
    public void ComplexMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }

    [Fact]
    public void ComplexMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        property.ValidationResult.Errors.Count.ShouldBe(1);
        property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
    }
    
    [Fact]
    public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var value = Helpers.ErrorStringValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var value = Helpers.ErrorIntValue;
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
                return new ValidatedRequiredProperty<RClassValueObject>((RClassValueObject?)null);
            });

        //Assert
        validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
                return new ValidatedRequiredProperty<RStructValueObject>((RStructValueObject?)null);
            });

        //Assert
        validatedProperty.ShouldBe(defaultValue);
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
                return new ValidatedRequiredProperty<RClassValueObject>((RClassValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
                return new ValidatedRequiredProperty<RStructValueObject>((RStructValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.ShouldBeFalse();
    }
    
    [Fact]
    public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
            return new ValidatedRequiredProperty<RClassValueObject>((RClassValueObject?)null);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    [Fact]
    public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            //It's okay to pass null as parameter here, as this code does not get called due to parameter being null
            return new ValidatedRequiredProperty<RStructValueObject>((RStructValueObject?)null);
        });

        //Assert
        nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

    #endregion
}