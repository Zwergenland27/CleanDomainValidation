using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;
using Tests.ApplicationTests.Enums.Required;

namespace Tests.ApplicationTests.Enums.WithDefault;

public class IntToEnumTests
{
    [Fact]
	public void Map_ShouldReturnEnumAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntNotNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.EnumOneInt;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenIntInvalid()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var value = Helpers.EnumInvalidInt;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(value);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntNull()
	{
		//Arrange
		var defaultValue = Helpers.DefaultEnumValue;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);

		var parameters = new RIntParameter(null);
		var property = new RequiredEnumWithDefaultProperty<RIntParameter, TestEnum>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(defaultValue);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}