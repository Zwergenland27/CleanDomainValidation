using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums.Required;

public class StringToEnumTests
{
	[Fact]
	public void Map_ShouldReturnEnumAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void Map_ShouldRemoveNameFromNameStack_WhenStringNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneString;
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
	public void Map_ShouldReturnDefaultAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenStringInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidString;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringParameter(value);
		var property = new RequiredEnumProperty<RStringParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultAndSetMissingErrorAndRemoveNameFromNameStack_WhenStringNull()
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
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}