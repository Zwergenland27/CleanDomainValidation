using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;
using Tests.ApplicationTests.Structs.Required;

namespace Tests.ApplicationTests.Structs.WithDefault;

public class ConstructorMappedTests
{
    #region Class
    [Fact]
    public void ConstructorMapClass_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
		//Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
		var value = Helpers.ExampleStringValue;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedValue.ShouldBe(new RClassValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

    [Fact]
    public void ConstructorMapClass_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
    {
        //Arrange
        var defaultValue = new RClassValueObject(Helpers.DefaultStringValue);
        var nameStack = new NameStack("");
        nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(null);
		var property = new RequiredStructWithDefaultProperty<RClassParameter, RClassValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedProperty.ShouldBe(defaultValue);
				
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }

	#endregion
	
	#region Struct
	
	[Fact]
	public void ConstructorMapStruct_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
		var value = Helpers.ExampleIntValue;
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedValue.ShouldBe(new RStructValueObject(value));
				
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ConstructorMapStruct_ShouldReturnDefaultValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var defaultValue = new RStructValueObject(Helpers.DefaultIntValue);
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(null);
		var property = new RequiredStructWithDefaultProperty<RStructParameter, RStructValueObject>(parameters, defaultValue, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedProperty.ShouldBe(defaultValue);
				
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}