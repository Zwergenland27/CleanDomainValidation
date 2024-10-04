using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Classes;

public record OClassParameter(string? Value) : IParameters;

public record OClassValueObject(string Value)
{
    public static CanFail<OClassValueObject> Create(string value)
    {
        if (value == "error") return Error.Validation("Validation.Error", "An error occured");
        return new OClassValueObject(value);
    }
}

public record OStructParameter(int? Value) : IParameters;

public record OStructValueObject(int Value)
{
    public static CanFail<OStructValueObject> Create(int value)
    {
        if (value == 9) return Error.Validation("Validation.Error", "An error occured");
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
        var value = "value";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.Should().Be(value);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = "value";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters);

        //Act
        _ = property.Map(p => p.Value);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void DirectMap_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value);

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void DirectMap_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var property = new OptionalClassProperty<OClassParameter, string>(parameters);

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
        var value = "value";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedValue = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        validatedValue.Should().Be(new OClassValueObject(value));
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = 1;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedValue = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        validatedValue.Should().Be(new OStructValueObject(value));
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = "value";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = 1;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = "error";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = 9;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = "error";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
    }

    [Fact]
    public void FactoryMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = 9;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
    }

    [Fact]
    public void FactoryMapClass_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value, OClassValueObject.Create);


        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void FactoryMapStruct_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value, OStructValueObject.Create);


        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void FactoryMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        _ = property.Map(p => p.Value, OClassValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        _ = property.Map(p => p.Value, OStructValueObject.Create);

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    #endregion

    #region Constructor Mapped

    [Fact]
    public void ConstructorMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
		var value = "value";
		var parameters = new OClassParameter(value);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedValue.Should().Be(new OClassValueObject(value));
	}

    [Fact]
    public void ConstructorMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
		//Arrange
		var value = 1;
		var parameters = new OStructParameter(value);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		validatedValue.Should().Be(new OStructValueObject(value));
	}

    [Fact]
	public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
		var value = "value";
		var parameters = new OClassParameter(value);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
		//Arrange
		var value = 1;
		var parameters = new OStructParameter(value);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

    [Fact]
    public void ConstructorMapClass_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
		var parameters = new OClassParameter(null);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ConstructorMapStruct_ShouldReturnNull_WhenValueNull()
    {
		//Arrange
        var parameters = new OStructParameter(null);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.Map(p => p.Value, v => new OStructValueObject(v));

        //Assert
        validatedProperty.Should().BeNull();
    }

    [Fact]
    public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
		var parameters = new OClassParameter(null);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

    [Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
		//Arrange
		var parameters = new OStructParameter(null);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Complex Mapped

	[Fact]
    public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = "value";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OClassValueObject>(new OClassValueObject(value));
            });


        //Assert
        validatedProperty.Should().Be(new OClassValueObject(value));
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnValueObject_WhenValueNotNull()
    {
        //Arrange
        var value = 1;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>(new OStructValueObject(value));
            });

        //Assert
        validatedProperty.Should().Be(new OStructValueObject(value));
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = "value";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(new OClassValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNotNull()
    {
        //Arrange
        var value = 1;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(new OStructValueObject(value));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = "error";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(Error.Validation("Error.Validation", "An error occured"));
        });

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = 9;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>(Error.Validation("Error.Validation", "An error occured"));
            });

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ComplexMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = "error";
        var parameters = new OClassParameter(value);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OClassValueObject>(Error.Validation("Error.Validation", "An error occured"));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Error.Validation", "An error occured"));
    }

    [Fact]
    public void ComplexMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
    {
        //Arrange
        var value = 9;
        var parameters = new OStructParameter(value);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
        {
            return new ValidatedOptionalClassProperty<OStructValueObject>(Error.Validation("Error.Validation", "An error occured"));
        });

        //Assert
        property.ValidationResult.HasFailed.Should().BeTrue();
        property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Error.Validation", "An error occured"));
    }

    [Fact]
    public void ComplexMapClass_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OClassValueObject>((OClassValueObject?)null);
            });

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ComplexMapStruct_ShouldReturnNull_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        var validatedProperty = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>((OStructValueObject?)null);
            });

        //Assert
        validatedProperty.Should().Be(null);
    }

    [Fact]
    public void ComplexMapClass_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OClassParameter(null);
        var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OClassValueObject>((OClassValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    [Fact]
    public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNull()
    {
        //Arrange
        var parameters = new OStructParameter(null);
        var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters);

        //Act
        _ = property.MapComplex(p => p.Value, builder =>
            {
                return new ValidatedOptionalClassProperty<OStructValueObject>((OStructValueObject?)null);
            });

        //Assert
        property.ValidationResult.HasFailed.Should().BeFalse();
    }

    #endregion
}