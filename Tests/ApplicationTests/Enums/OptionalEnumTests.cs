using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Enums;

public record OStringParameter(string? Value) : IParameters;

public record OStringListParameter(List<string>? Value) : IParameters;

public record OIntParameter(int? Value) : IParameters;

public record OIntListParameter(List<int>? Value) : IParameters;

public enum OTestEnum
{
	One,
}

public class OptionalEnumTests
{
	private Error _invalidEnumError = Error.Validation("Enum.Invalid", "The enum is invalid");

	[Fact]
	public void IsRequired_Should_BeFalse()
	{
		//Arrange
		var value = "One";
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Assert
		property.IsRequired.Should().BeFalse();
	}

	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(OTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_IsMissingShouldBeFalse_WhenStringNotNull()
	{
		//Arrange
		var value = "One";
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var value = "Invalid";
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void Map_ShouldReturnNull_WhenStringNull()
	{
		//Arrange
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNull()
	{
		//Arrange
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_IsMissingShouldBeTrue_WhenStringNull()
	{
		//Arrange
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

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
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(OTestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_IsMissingShouldBeFalse_WhenIntNotNull()
	{
		//Arrange
		var value = 0;
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeFalse();
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var value = 1;
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(_invalidEnumError);
	}

	[Fact]
	public void Map_ShouldReturnNull_WhenIntNull()
	{
		//Arrange
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		var validatedProperty = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		validatedProperty.Should().Be(null);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNull()
	{
		//Arrange
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void Map_IsMissingShouldBeTrue_WhenIntNull()
	{
		//Arrange
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, OTestEnum>(parameters);

		//Act
		_ = property.Map(p => p.Value, _invalidEnumError);

		//Assert
		property.IsMissing.Should().BeTrue();
	}

	#endregion
}
