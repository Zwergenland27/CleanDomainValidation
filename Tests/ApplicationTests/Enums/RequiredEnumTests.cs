using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums;

public record RStringParameter(string? Value) : IParameters;

public record RIntParameter(int? Value) : IParameters;

public class RequiredEnumTests
{ 
	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.ExampleEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.ExampleEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.ExampleEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefault_WhenStringInvalid()
	{
		//Arrange
		var value = Helpers.InvalidEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
	}
	
	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var value = Helpers.InvalidEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

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
		var value = Helpers.InvalidEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefault_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
	}

	[Fact]
	public void Map_ShouldSetErrors_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(null);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

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
		var value = Helpers.ExampleEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.ExampleEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldReturnDefault_WhenIntInvalid()
	{
		//Arrange
		var value = Helpers.InvalidEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.ExampleEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var value = Helpers.InvalidEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

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
		var value = Helpers.InvalidEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefault_WhenIntNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
	}

	[Fact]
	public void Map_ShouldSetErrors_WhenIntNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(null);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
}
