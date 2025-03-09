using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums;

public record OStringParameter(string? Value) : IParameters;

public record OIntParameter(int? Value) : IParameters;

public class OptionalEnumTests
{
	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldShouldRemoveNameFromNameStack_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNull_WhenStringInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(null);
	}
	
	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(value);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNull_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion

	#region Int to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNull_WhenIntInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNull_WhenIntNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
}
