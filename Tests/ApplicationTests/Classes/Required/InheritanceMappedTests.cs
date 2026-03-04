using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using Shouldly;

namespace Tests.ApplicationTests.Classes.Required;

public interface IBaseClass;
public record ConcreteClass : IBaseClass;

public record Parameter(IBaseParameter? BaseParameter);
public interface IBaseParameter;
public record ConcreteClassParameter : IBaseParameter;

public class InheritanceMappedTests
{
    [Fact]
	public void InheritanceMapClass_ShouldPassNameStackWithPropertyNameToBuilderAndReturnValueObjectAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new Parameter(new ConcreteClassParameter());
		var property = new RequiredClassProperty<Parameter, IBaseClass>(parameters, Helpers.ExampleMissingError, nameStack);
		
		//Act
		var validatedProperty = property.MapInherited(p => p.BaseParameter, registry => 
			registry.Register<ConcreteClassParameter, ConcreteClass>(builder =>
			{
				//Assert
				builder.NameStackShouldPeekPropertyName(new PropertyNameEntry(Helpers.PropertyName));
				return new ValidatedRequiredProperty<ConcreteClass>(new ConcreteClass());
			}));


		//Assert
		validatedProperty.ShouldBe(new ConcreteClass());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}