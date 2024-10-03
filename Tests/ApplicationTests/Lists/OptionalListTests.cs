using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Lists;

public record OClassListParameter(List<string>? Value) : IParameters;

public record OClassValueObject(string Value)
{
	public static CanFail<OClassValueObject> Create(string value)
	{
		if (value == "error") return Error.Validation("Validation.Error", "An error occured");
		return new OClassValueObject(value);
	}
}

public record OStructListParameter(List<int>? Value) : IParameters;

public record OStructValueObject(int Value)
{
	public static CanFail<OStructValueObject> Create(int value)
	{
		if (value == 9) return Error.Validation("Validation.Error", "An error occured");
		return new OStructValueObject(value);
	}
}

public enum OTestEnum
{
	One,
	Two,
	Three
}

public record OStringListParameter(List<string>? Value) : IParameters;

public record OIntListParameter(List<int>? Value) : IParameters;

public class OptionalListTests
{
	private static Error _invalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");

	#region Direct Mapped

	[Fact]
	public void DirectMapEachClass_ShouldReturnList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, string>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeEquivalentTo(value);
	}

	[Fact]
	public void DirectMapEachStruct_ShouldReturnList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, int>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeEquivalentTo(value);
	}

	[Fact]
	public void DirectMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, string>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, int>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, string>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void DirectMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, int>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void DirectMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, string>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, int>(parameters);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Factory Mapped

	[Fact]
	public void FactoryMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(value.Select(OClassValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(value.Select(OStructValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachClass_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = ["value1", "error"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<int> value = [1, 9];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void FactoryMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Constructor Mapped

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OClassValueObject(v));

		//Assert
		result.Should().BeEquivalentTo(value.Select(OClassValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OStructValueObject(v));

		//Assert
		result.Should().BeEquivalentTo(value.Select(OStructValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OClassValueObject(v));

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OStructValueObject(v));

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, v => new OStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Complex Mapped

	[Fact]
	public void ComplexMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()	
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(value.Select(OClassValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(value.Select(OStructValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachClass_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = ["value1", "error"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<int> value = [1, 9];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(value));
		});

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(value));
		});

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void ComplexMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Enums

	[Fact]
	public void MapEachEnum_ShouldReturnEnumList_WhenStringListIsNotNull()
	{
		//Arrange
		List<string> value = ["One", "Two"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		result.Should().BeEquivalentTo([OTestEnum.One, OTestEnum.Two]);
	}

	[Fact]
	public void MapEachEnum_ShouldReturnEnumList_WhenIntListIsNotNull()
	{
		//Arrange
		List<int> value = [0, 1];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		result.Should().BeEquivalentTo([OTestEnum.One, OTestEnum.Two]);
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenStringListIsNotNull()
	{
		//Arrange
		List<string> value = ["One", "Two"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenIntListIsNotNull()
	{
		//Arrange
		List<int> value = [0, 1];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldSetInvalidEnumError_WhenAtLeasOneStringIsInvalidEnum()
	{
		//Arrange
		List<string> value = ["One", "Four"];
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void MapEachEnum_ShouldSetInvalidEnumError_WhenAtLeasOneIntIsInvalidEnum()
	{
		//Arrange
		List<int> value = [0, 3];
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void MapEachEnum_ShouldReturnNull_WhenStringListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void MapEachEnum_ShouldReturnNull_WhenIntListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenStringListIsNull()
	{
		//Arrange
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenIntListIsNull()
	{
		//Arrange
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OTestEnum>(parameters);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion
}
