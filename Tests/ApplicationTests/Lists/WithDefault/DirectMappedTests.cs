using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;
using Tests.ApplicationTests.Lists.Required;

namespace Tests.ApplicationTests.Lists.WithDefault;

public class DirectMappedTests
{
	#region Class

	[Fact]
	public void DirectMapEachClass_ShouldReturnListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> defaultList = [Helpers.DefaultStringValue, Helpers.DefaultStringAlternateValue];
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, string>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeEquivalentTo(value);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void DirectMapEachClass_ShouldReturnDefaultListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		List<string> defaultList = [Helpers.DefaultStringValue, Helpers.DefaultStringAlternateValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, string>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void DirectMapEachStruct_ShouldReturnListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> defaultList = [Helpers.DefaultIntValue, Helpers.DefaultIntAlternateValue];
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, int>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeEquivalentTo(value);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void DirectMapEachStruct_ShouldReturnDefaultListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		List<int> defaultList = [Helpers.DefaultIntValue, Helpers.DefaultIntAlternateValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, int>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}