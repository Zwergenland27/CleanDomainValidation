using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums.Required;

public class IntToEnumTests
{
    [Fact]
	public void Map_ShouldReturnEnumAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

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
		var value = Helpers.EnumInvalidInt;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntParameter(value);
		var property = new RequiredEnumProperty<RIntParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnDefaultAndSetMissingErrorAndRemoveNameFromNameStack_WhenIntNull()
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
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}