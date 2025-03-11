using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.Structs.Optional;

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
		var parameters = new OClassParameter(value);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, OClassValueObject.Create);

		//Assert
		validatedValue.ShouldBe(new OClassValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(value);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, OClassValueObject.Create);

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(null);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, OClassValueObject.Create);


		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
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
		var parameters = new OStructParameter(value);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, OStructValueObject.Create);

		//Assert
		validatedValue.ShouldBe(new OStructValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapStruct_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(value);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, OStructValueObject.Create);

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(null);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, OStructValueObject.Create);


		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}