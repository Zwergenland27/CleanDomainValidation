using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Enums;

public class RequiredEnumWithDefaultTests
{
	private static Error InvalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");

	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, RTestEnum>(parameters, defaultValue);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(RTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, RTestEnum>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var value = "Invalid";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, RTestEnum>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(InvalidEnumError);
	}

	[Fact]
	public void Map_ShouldReturnDefaultValue_WhenStringNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, RTestEnum>(parameters, defaultValue);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(defaultValue);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, RTestEnum>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Int to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenIntNotNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, RTestEnum>(parameters, defaultValue);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(RTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, RTestEnum>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}


	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var value = 2;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, RTestEnum>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(InvalidEnumError);
	}

	[Fact]
	public void Map_ShouldReturnDefaultValue_WhenIntNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, RTestEnum>(parameters, defaultValue);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(defaultValue);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNull()
	{
		//Arrange
		var defaultValue = RTestEnum.Two;
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, RTestEnum>(parameters, defaultValue);

		//Act
		_ = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion
}