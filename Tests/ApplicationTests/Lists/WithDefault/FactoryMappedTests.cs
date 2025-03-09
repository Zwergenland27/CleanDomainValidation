using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;
using Tests.ApplicationTests.Lists.Required;
using RClassValueObject = Tests.ApplicationTests.Classes.RClassValueObject;
using RStructValueObject = Tests.ApplicationTests.Classes.RStructValueObject;

namespace Tests.ApplicationTests.Lists.WithDefault;

public class FactoryMappedTests
{
    #region Class

	[Fact]
	public void FactoryMapEachClass_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new (Helpers.DefaultStringValue), new (Helpers.DefaultStringAlternateValue)];
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.ShouldBeEquivalentTo(value.Select(RClassValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new (Helpers.DefaultStringValue), new (Helpers.DefaultStringAlternateValue)];
		List<string> value = [Helpers.ExampleStringValue, Helpers.ErrorStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnDefaultListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new (Helpers.DefaultStringValue), new (Helpers.DefaultStringAlternateValue)];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void FactoryMapEachStruct_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (Helpers.DefaultIntValue), new (Helpers.DefaultIntAlternateValue)];
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.ShouldBeEquivalentTo(value.Select(RStructValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapEachStruct_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (Helpers.DefaultIntValue), new (Helpers.DefaultIntAlternateValue)];
		List<int> value = [Helpers.ExampleIntValue, Helpers.ErrorIntValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);

		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void FactoryMapEachStruct_ShouldReturnDefaultListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (Helpers.DefaultIntValue), new (Helpers.DefaultIntAlternateValue)];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}