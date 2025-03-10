using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.Structs.Optional;

public class ConstructorMappedTests
{
    #region Class

	[Fact]
	public void ConstructorMapClass_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(value);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedProperty.ShouldBe(new OClassValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(null);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void ConstructorMapStruct_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(value);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		validatedProperty.ShouldBe(new OStructValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ConstructorMapStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(null);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}