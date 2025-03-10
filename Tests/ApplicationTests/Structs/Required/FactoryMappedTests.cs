using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.Structs.Required;

public class FactoryMappedTests
{
    #region Class

	[Fact]
	public void FactoryMapClass_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredStructProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		validatedValue.ShouldBe(new RClassValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnDefaultAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredStructProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		validatedProperty.ShouldBe(default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnDefaultAndSetMissingErrorAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(null);
		var property = new RequiredStructProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);


		//Assert
		validatedProperty.ShouldBe(default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void FactoryMapStruct_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredStructProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		validatedValue.ShouldBe(new RStructValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapStruct_ShouldReturnDefaultAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredStructProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		validatedProperty.ShouldBe( default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapStruct_ShouldReturnDefaultAndSetMissingErrorAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(null);
		var property = new RequiredStructProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);


		//Assert
		validatedProperty.ShouldBe( default);
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}