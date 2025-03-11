using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Classes.Optional;

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
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new OClassValueObject(v));

		//Assert
		validatedValue.ShouldBe(new OClassValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

    [Fact]
    public void ConstructorMapClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
		var parameters = new OClassParameter(null);
        var nameStack = new NamingStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var property = new OptionalClassProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

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
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		validatedValue.ShouldBe(new OStructValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ConstructorMapStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new OStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new OptionalClassProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new OStructValueObject(v));

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}