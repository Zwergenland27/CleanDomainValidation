using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Enums;

public record RStringParameter(string? Value) : IParameters;

public record RStringListParameter(List<string>? Value) : IParameters;

public record RIntParameter(int? Value) : IParameters;

public record RIntListParameter(List<int>? Value) : IParameters;

public enum RTestEnum
{
	One,
}

public class RequiredEnumTests
{
	private static Error _missingError => Error.Validation("Enum.Missing", "The enum is missing");
	private static Error _invalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");

	[Fact]
	public void IsRequired_Should_BeTrue()
	{
		//Arrange
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Assert
		property.IsRequired.Should().BeTrue();
	}

	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(RTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_IsMissingShouldBeFalse_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var value = "Invalid";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void Map_ShoulReturnDefault_WhenStringNull()
	{
		//Arrange
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(default);
	}

	[Fact]
	public void Map_ShouldSetErrors_WhenStringNull()
	{
		//Arrange
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void Map_IsMissingShouldBeTrue_WhenStringNull()
	{
		//Arrange
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion

	#region Int to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(RTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_IsMissingShouldBeFalse_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var value = 1;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void Map_ShoulReturnDefault_WhenIntNull()
	{
		//Arrange
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(default);
	}

	[Fact]
	public void Map_ShouldSetErrors_WhenIntNull()
	{
		//Arrange
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_missingError);
	}

	[Fact]
	public void Map_IsMissingShouldBeTrue_WhenIntNull()
	{
		//Arrange
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, _missingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion
}
