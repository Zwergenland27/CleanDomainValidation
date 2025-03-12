using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Enums.Optional;

public class IntToEnumTests
{
	[Fact]
	public void Map_ShouldReturnEnumAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntNotNull()
	{
		//Arrange
		var value = Helpers.EnumOneInt;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBe(TestEnum.One);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	[Fact]
	public void Map_ShouldReturnNullAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenIntInvalid()
	{
		//Arrange
		var value = Helpers.EnumInvalidInt;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(value);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void Map_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OIntParameter(null);
		var property = new OptionalEnumProperty<OIntParameter, TestEnum>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}