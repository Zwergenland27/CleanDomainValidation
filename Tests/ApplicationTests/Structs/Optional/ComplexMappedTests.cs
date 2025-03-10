using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.Structs.Optional;

public class ComplexMappedTests
{
    #region Class

	[Fact]
	public void ComplexMapClass_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(value);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalStructProperty<OClassValueObject>(new OClassValueObject(value));
		});


		//Assert
		validatedProperty.ShouldBe(new OClassValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapClass_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(value);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalStructProperty<OClassValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassParameter(null);
		var property = new OptionalStructProperty<OClassParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			//It's okay to pass null as parameter here, as this code does not get called due to parameter being null
			return new ValidatedOptionalStructProperty<OClassValueObject>((OClassValueObject?) null);
		});

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void ComplexMapStruct_ShouldReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(value);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalStructProperty<OStructValueObject>(new OStructValueObject(value));
		});

		//Assert
		validatedProperty.ShouldBe(new OStructValueObject(value));
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapStruct_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(value);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedOptionalStructProperty<OStructValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(null);
		var property = new OptionalStructProperty<OStructParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			//It's okay to pass null as parameter here, as this code does not get called due to parameter being null
			return new ValidatedOptionalStructProperty<OStructValueObject>((OStructValueObject?) null);
		});

		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}