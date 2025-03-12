using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Optional;

public class ComplexMappedTests
{
	#region Class

	[Fact]
	public void ComplexMapEachClass_ShouldPassNameStackWithPropertyNameToBuilderAndReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()	
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			//Assert
			builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
			
			var voValue = builder.ClassProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeEquivalentTo(value.Select(OClassValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapEachClass_ShouldPassNameStackWithPropertyNameToBuilderAndReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.ErrorStringValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			//Assert
			builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
			
			var voValue = builder.ClassProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.ClassProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => OClassValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
	
	#region Struct
	
	[Fact]
	public void ComplexMapEachStruct_ShouldPassNameStackWithPropertyNameToBuilderAndReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			//Assert
			builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
			
			var voValue = builder.StructProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeEquivalentTo(value.Select(OStructValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapEachStruct_ShouldPassNameStackWithPropertyNameToBuilderAndReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.ErrorIntValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			//Assert
			builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
			
			var voValue = builder.StructProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapEachStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.StructProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => OStructValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}