﻿using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Classes;

public record RClassParameter(string? Value) : IParameters;

public record RClassValueObject(string Value)
{
	public static CanFail<RClassValueObject> Create(string value)
	{
		if (value == "error") return Error.Validation("Validation.Error", "An error occured");
		return new RClassValueObject(value);
	}
}

public record RStructParameter(int? Value) : IParameters;
public record RStructValueObject(int Value)
{
	public static CanFail<RStructValueObject> Create(int value)
	{
		if (value == 9) return Error.Validation("Validation.Error", "An error occured");
		return new RStructValueObject(value);
	}
}

public class RequiredClassTests
{
	private static Error _missing => Error.Validation("Error.Missing", "The value is missing");

	#region Direct Mapped

	[Fact]
	public void DirectMap_ShouldReturnValue_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, _missing);

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
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMap_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void DirectMap_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	#endregion

	#region Factory Mapped

	[Fact]
	public void FactoryMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		var validatedValue = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		validatedValue.Should().Be(new RClassValueObject(value));
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		var validatedValue = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		validatedValue.Should().Be(new RStructValueObject(value));
	}

	[Fact]
	public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = "error";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = 9;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = "error";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

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
		var value = 9;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);


		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);


		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapClass_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	[Fact]
	public void FactoryMapStruct_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	#endregion

	#region Constructor Mapped

	[Fact]
	public void ConstructorMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedValue.Should().Be(new RClassValueObject(value));
	}

	[Fact]
	public void ConstructorMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedValue.Should().Be(new RStructValueObject(value));
	}

	[Fact]
	public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ConstructorMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ConstructorMapClass_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	[Fact]
	public void ConstructorMapStruct_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	#endregion

	#region Complex Mapped

	[Fact]
	public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

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
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

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
		var value = "value";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

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
		var value = 1;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

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
		var value = "error";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

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
		var value = 9;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

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
		var value = "error";
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

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
		var value = 9;
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

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
	public void ComplexMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(_missing);
		});

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(_missing);
		});

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ComplexMapClass_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, _missing);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(_missing);
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	[Fact]
	public void ComplexMapStruct_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, _missing);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(_missing);
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missing);
	}

	#endregion
}
