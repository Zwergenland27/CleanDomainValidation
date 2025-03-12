using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;
using Tests.ApplicationTests.Enums.Required;

namespace Tests.ApplicationTests.Enums.WithDefault;

public class StringToEnumTests
{
	[Fact]
	public void Map_ShouldReturnEnumAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.EnumOneString;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void Map_ShouldReturnDefaultAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenStringInvalid()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.EnumInvalidString;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RStringParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RStringParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(defaultValue);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}