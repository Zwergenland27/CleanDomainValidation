using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums.Optional;

public class StringToEnumTests
{
	[Fact]
	public void Map_ShouldReturnEnumAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringNotNull()
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
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNullAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenStringInvalid()
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
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStringParameter(null);
		var property = new OptionalEnumProperty<OStringParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}