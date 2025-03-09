using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;
using Tests.ApplicationTests.Lists.Required;

namespace Tests.ApplicationTests.Lists.WithDefault;

public class ConstructorMappedTests
{
    #region Class

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new (Helpers.DefaultStringValue), new (Helpers.DefaultStringAlternateValue)];
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(value.Select(RClassValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new (Helpers.DefaultStringValue), new (Helpers.DefaultStringAlternateValue)];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (Helpers.DefaultIntValue), new (Helpers.DefaultIntAlternateValue)];
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(value.Select(RStructValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (Helpers.DefaultIntValue), new (Helpers.DefaultIntAlternateValue)];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	#endregion
}