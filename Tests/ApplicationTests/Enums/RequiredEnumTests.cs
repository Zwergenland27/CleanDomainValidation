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
	Two,
}

public class RequiredEnumTests
{
	private static Error MissingError => Error.Validation("Enum.Missing", "The enum is missing");
	private static Error InvalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");

	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(RTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var value = "Invalid";
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(InvalidEnumError);
	}

	[Fact]
	public void Map_ShoulReturnDefault_WhenStringNull()
	{
		//Arrange
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(default);
	}

	[Fact]
	public void Map_ShouldSetErrors_WhenStringNull()
	{
		//Arrange
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(MissingError);
	}

	#endregion

	#region Int to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(RTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var value = 2;
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(InvalidEnumError);
	}

	[Fact]
	public void Map_ShoulReturnDefault_WhenIntNull()
	{
		//Arrange
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		validatedProperty.Should().Be(default);
	}

	[Fact]
	public void Map_ShouldSetErrors_WhenIntNull()
	{
		//Arrange
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, RTestEnum>(parameters, MissingError);

		//Act
		var validatedProperty = property.Map(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(MissingError);
	}

	#endregion
}
