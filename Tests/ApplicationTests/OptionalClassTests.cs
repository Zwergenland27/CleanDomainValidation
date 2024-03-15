using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests;

public record ClassParameter(string? Value) : IParameters;

public record DirectMappedRequest(string? OptionalClassProperty) : IRequest;

public record ClassValueObject(string Value)
{
	public static CanFail<ClassValueObject> Create(string value)
	{
		if (value == "error") return Error.Validation("Validation.Error", "An error occured");
		return new ClassValueObject(value);
	}
}

public record FactoryMappedClassRequest(ClassValueObject? OptionalClassProperty) : IRequest;

public record StructParameter(int? Value) : IParameters;

public record FactoryMappedStructRequest(StructValueObject OptionalClassProperty) : IRequest;
public record StructValueObject(int Value)
{
	public static CanFail<StructValueObject> Create(int value)
	{
		if (value == 9) return Error.Validation("Validation.Error", "An error occured");
		return new StructValueObject(value);
	}
}

public class OptionalClassTests
{
	[Fact]
	public void IsRequired_Should_BeFalse()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

		//Assert
		property.IsRequired.Should().BeFalse();
	}
	#region Direct Mapped

	[Fact]
	public void DirectMap_ShouldReturnValue_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

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
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMap_IsMissingShouldBeFalse_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void DirectMap_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void DirectMap_ShouldNotSetErrors_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMap_IsMissingShouldBeTrue_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, string>(parameters);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion

	#region Factory Mapped

	[Fact]
	public void FactoryMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedValue = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		validatedValue.Should().Be(new ClassValueObject(value));
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedValue = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		validatedValue.Should().Be(new StructValueObject(value));
	}

	[Fact]
	public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapClass_IsMissingShouldBeFalse_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapStruct_IsMissingShouldBeFalse_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = "error";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = 9;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = "error";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = 9;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, ClassValueObject.Create);


		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new StructParameter(null);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, StructValueObject.Create);


		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void FactoryMapClass_ShouldNotSetErrors_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNull()
	{
		//Arrange
		var parameters = new StructParameter(null);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapClass_IsMissingShouldBeTrue_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, ClassValueObject.Create);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	[Fact]
	public void FactoryMapStruct_IsMissingShouldBeTrue_WhenValueNull()
	{
		//Arrange
		var parameters = new StructParameter(null);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.Map(p => p.Value, StructValueObject.Create);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion

	#region Complex Mapped

	[Fact]
	public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<ClassValueObject>(new ClassValueObject(value));
			});


		//Assert
		validatedProperty.Should().Be(new ClassValueObject(value));
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<StructValueObject>(new StructValueObject(value));
			});

		//Assert
		validatedProperty.Should().Be(new StructValueObject(value));
	}

	[Fact]
	public void ComplexMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<ClassValueObject>(new ClassValueObject(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<StructValueObject>(new StructValueObject(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapClass_IsMissingShouldBeFalse_WhenValueNotNull()
	{
		//Arrange
		var value = "value";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<ClassValueObject>(new ClassValueObject(value));
			});

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapStruct_IsMissingShouldBeFalse_WhenValueNotNull()
	{
		//Arrange
		var value = 1;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<StructValueObject>(new StructValueObject(value));
		});

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = "error";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<ClassValueObject>(Error.Validation("Error.Validation", "An error occured"));
		});

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = 9;
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<StructValueObject>(Error.Validation("Error.Validation", "An error occured"));
			});

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ComplexMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = "error";
		var parameters = new ClassParameter(value);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<ClassValueObject>(Error.Validation("Error.Validation", "An error occured"));
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
		var parameters = new StructParameter(value);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<StructValueObject>(Error.Validation("Error.Validation", "An error occured"));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Error.Validation", "An error occured"));
	}

	[Fact]
	public void ComplexMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<ClassValueObject>((ClassValueObject?) null);
			});

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new StructParameter(null);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<StructValueObject>((StructValueObject?) null);
			});

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void ComplexMapClass_ShouldNotSetErrors_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<ClassValueObject>((ClassValueObject?) null);
			});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNull()
	{
		//Arrange
		var parameters = new StructParameter(null);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
			{
				return new ValidatedOptionalProperty<StructValueObject>((StructValueObject?) null);
			});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapClass_IsMissingShouldBeTrue_WhenValueNull()
	{
		//Arrange
		var parameters = new ClassParameter(null);
		var property = new OptionalClassProperty<ClassParameter, ClassValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<ClassValueObject>((ClassValueObject?)null);
		});

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	[Fact]
	public void ComplexMapStruct_IsMissingShouldBeTrue_WhenValueNull()
	{
		//Arrange
		var parameters = new StructParameter(null);
		var property = new OptionalClassProperty<StructParameter, StructValueObject>(parameters);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalProperty<StructValueObject>((StructValueObject?)null);
		});

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion
}