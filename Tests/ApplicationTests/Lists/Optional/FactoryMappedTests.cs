using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Optional;

public class FactoryMappedTests
{
	#region Class

	[Fact]
	public void FactoryMapEachClass_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		result.ShouldBeEquivalentTo(value.Select(OClassValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.ErrorStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, OClassValueObject.Create);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void FactoryMapEachStruct_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		result.ShouldBeEquivalentTo(value.Select(OStructValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapEachStruct_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<int> value = [1, 9];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapEachStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, OStructValueObject.Create);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}