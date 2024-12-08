using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Classes;

public class RequiredClassWithDefaultTests
{
    #region Direct Mapped

    [Fact]
    public void DirectMap_ShouldReturnValue_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = "default";
        var value = "value";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.Should().Be(value);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = "default";
        var value = "value";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void DirectMap_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = "default";
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = "default";
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, string>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    #endregion

    #region Factory Mapped

    [Fact]
    public void FactoryMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "value";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedValue = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        validatedValue.Should().Be(new RClassValueObject(value));
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 1;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedValue = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        validatedValue.Should().Be(new RStructValueObject(value));
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "value";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 1;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "error";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 9;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "error";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
    }

    [Fact]
    public void FactoryMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 9;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);


        //Assert
        validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);


        //Assert
        validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value, RClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        _ = property.Map(p => p.Value, RStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    #endregion
    
    #region Constructor Mapped
    [Fact]
    public void ConstructorMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject("default");
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedValue.Should().Be(new RClassValueObject(value));
	}

    [Fact]
    public void ConstructorMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(3);
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedValue.Should().Be(new RStructValueObject(value));
	}

    [Fact]
	public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject("default");
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(3);
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

    [Fact]
    public void ConstructorMapClass_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
		var parameters = new RClassParameter(null);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void ConstructorMapStruct_ShouldReturnDefaultValue_WhenValueNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(3);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.Map(p => p.Value, v => new RStructValueObject(v));

        //Assert
        validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject("default");
		var parameters = new RClassParameter(null);
		var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
        var defaultValue = new RStructValueObject(3);
		var parameters = new RStructParameter(null);
		var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion
    
    #region Complex Mapped

	[Fact]
    public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "value";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
            });


        //Assert
        validatedProperty.Should().Be(new RClassValueObject(value));
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 1;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
            });

        //Assert
        validatedProperty.Should().Be(new RStructValueObject(value));
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "value";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 1;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "error";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(Error.Validation("Error.Validation", "An error occured"));
        });

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 9;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RStructValueObject>(Error.Validation("Error.Validation", "An error occured"));
            });

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ComplexMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var value = "error";
        var parameters = new RClassParameter(value);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RClassValueObject>(Error.Validation("Error.Validation", "An error occured"));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Error.Validation", "An error occured"));
    }

    [Fact]
    public void ComplexMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var value = 9;
        var parameters = new RStructParameter(value);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedRequiredProperty<RStructValueObject>(Error.Validation("Error.Validation", "An error occured"));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Error.Validation", "An error occured"));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RClassValueObject>((RClassValueObject?)null);
            });

        //Assert
        validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnDefaultValue_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RStructValueObject>((RStructValueObject?)null);
            });

        //Assert
        validatedProperty.Should().Be(defaultValue);
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject("default");
        var parameters = new RClassParameter(null);
        var property = new RequiredClassWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RClassValueObject>((RClassValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RStructValueObject(3);
        var parameters = new RStructParameter(null);
        var property = new RequiredClassWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedRequiredProperty<RStructValueObject>((RStructValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    #endregion
}