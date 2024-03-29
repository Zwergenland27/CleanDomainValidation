using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Lists;

public record RClassListParameter(List<string>? Value) : IParameters;

public record RClassValueObject(string Value)
{
	public static CanFail<RClassValueObject> Create(string value)
	{
		if (value == "error") return Error.Validation("Validation.Error", "An error occured");
		return new RClassValueObject(value);
	}
}

public record RStructListParameter(List<int>? Value) : IParameters;

public record RStructValueObject(int Value)
{
	public static CanFail<RStructValueObject> Create(int value)
	{
		if (value == 9) return Error.Validation("Validation.Error", "An error occured");
		return new RStructValueObject(value);
	}
}

public enum RTestEnum
{
	One,
	Two,
	Three
}

public record RStringListParameter(List<string>? Value) : IParameters;

public record RIntListParameter(List<int>? Value) : IParameters;


public class RequiredListTests
{
	private static Error _missingError = Error.Validation("Error.Missing", "The value is missing");
	private static Error _invalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");

	[Fact]
	public void IsRequired_Should_BeTrue()
	{
		//Arrange
		List<string> value = ["value1"];
		var parameters = new OClassListParameter(value);
		var property = new RequiredListProperty<OClassListParameter, string>(parameters, _missingError);

		//Assert
		property.IsRequired.Should().BeTrue();
	}

	#region Direct Mapped

	[Fact]
	public void DirectMapEachClass_ShouldReturnList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, string>(parameters, _missingError);

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
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, int>(parameters, _missingError);

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
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, string>(parameters, _missingError);

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
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, int>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachClass_IsMissingShouldBeFalse_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, string>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachStruct_IsMissingShouldBeFalse_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, int>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, string>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void DirectMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, int>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void DirectMapEachClass_ShouldSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, string>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void DirectMapEachStruct_ShouldSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, int>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void DirectMapEachClass_IsMissingShouldBeTrue_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, string>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	[Fact]
	public void DirectMapEachStruct_IsMissingShouldBeTrue_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, int>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion

	#region Factory Mapped

	[Fact]
	public void FactoryMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(value.Select(v => RClassValueObject.Create(v).Value));
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(value.Select(v => RStructValueObject.Create(v).Value));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachClass_IsMissingShouldBeFalse_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachStruct_IsMissingShouldBeFalse_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachClass_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = ["value1", "error"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<int> value = [1, 9];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void FactoryMapEachClass_ShouldSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void FactoryMapEachClass_IsMissingShouldBeTrue_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	[Fact]
	public void FactoryMapEachStruct_IsMissingShouldBeTrue_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion

	#region Compex Mapped

	[Fact]
	public void ComplexMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(value.Select(v => RClassValueObject.Create(v).Value));
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(value.Select(v => RStructValueObject.Create(v).Value));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachClass_IsMissingShouldBeFalse_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachStruct_IsMissingShouldBeFalse_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachClass_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = ["value1", "error"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
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
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldReturnNull_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void ComplexMapEachClass_ShouldSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void ComplexMapEachClass_IsMissingShouldBeTrue_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	[Fact]
	public void ComplexMapEachStruct_IsMissingShouldBeTrue_WhenParameterListIsNull()
	{
		//Arrange
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, _missingError);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion

	#region Enum

	[Fact]
	public void MapEachEnum_ShouldReturnEnumList_WhenStringListIsNotNull()
	{
		//Arrange
		List<string> value = ["One", "Two"];
		var parameters = new RStringListParameter(value);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

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
		var parameters = new RIntListParameter(value);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

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
		var parameters = new RStringListParameter(value);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

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
		var parameters = new RIntListParameter(value);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_IsMissingShouldBeFalse_WhenStringListIsNotNull()
	{
		//Arrange
		List<string> value = ["One", "Two"];
		var parameters = new RStringListParameter(value);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_IsMissingShouldBeFalse_WhenIntListIsNotNull()
	{
		//Arrange
		List<int> value = [0, 1];
		var parameters = new RIntListParameter(value);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldSetInvalidEnumError_WhenAtLeastOneStringIsInvalidEnum()
	{
		//Arrange
		List<string> value = ["One", "Invalid"];
		var parameters = new RStringListParameter(value);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void MapEachEnum_ShouldSetInvalidEnumError_WhenAtLeastOneIntIsInvalidEnum()
	{
		//Arrange
		List<int> value = [0, 3];
		var parameters = new RIntListParameter(value);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

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
		var parameters = new RStringListParameter(null);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void MapEachEnum_ShouldReturnNull_WhenIntListIsNull()
	{
		//Arrange
		var parameters = new RIntListParameter(null);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public void MapEachEnum_ShouldSetErrors_WhenStringListIsNull()
	{
		//Arrange
		var parameters = new RStringListParameter(null);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void MapEachEnum_ShouldSetErrors_WhenIntListIsNull()
	{
		//Arrange
		var parameters = new RIntListParameter(null);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void MapEachEnum_IsMissingShouldBeTrue_WhenStringListIsNull()
	{
		//Arrange
		var parameters = new RStringListParameter(null);
		var property = new RequiredListProperty<RStringListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	[Fact]
	public void MapEachEnum_IsMissingShouldBeTrue_WhenIntListIsNull()
	{
		//Arrange
		var parameters = new RIntListParameter(null);
		var property = new RequiredListProperty<RIntListParameter, RTestEnum>(parameters, _missingError);

		//Act
		var result = property.MapEach(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion
}
