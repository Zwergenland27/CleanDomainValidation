using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Required;

public class ConstructorMappedTests
{
	#region Class

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(value.Select(v => new RClassValueObject(v)).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnNullAndSetMissingErrorAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
	
	#region Struct
	
	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(value.Select(v => new RStructValueObject(v)).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnNullAndSetMissingErrorAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}