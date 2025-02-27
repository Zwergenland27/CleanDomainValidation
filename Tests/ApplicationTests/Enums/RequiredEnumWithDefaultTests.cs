using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums;

public class RequiredEnumWithDefaultTests
{
	#region String to enum

	[Fact]
	public void Map_ShouldReturnEnum_WhenStringNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.ExampleEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.ExampleEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.ExampleEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void Map_ShouldReturnDefault_WhenStringInvalid()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.InvalidEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
	}
	
	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenStringInvalid()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.InvalidEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

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
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.InvalidEnumStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultValue_WhenStringNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(defaultValue);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenStringNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

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
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.ExampleEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.ExampleEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.ExampleEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefault_WhenIntInvalid()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.InvalidEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
	}

	[Fact]
	public void Map_ShouldSetInvalidEnumError_WhenIntInvalid()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.InvalidEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

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
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.InvalidEnumIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultValue_WhenIntNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(defaultValue);
	}

	[Fact]
	public void Map_ShouldNotSetErrors_WhenIntNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenIntNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		_ = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));

	}

	#endregion
}