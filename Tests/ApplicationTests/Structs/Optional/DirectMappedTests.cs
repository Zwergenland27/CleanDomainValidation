using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests.Structs.Optional;

public class DirectMappedTests
{
    [Fact] 
    public void DirectMap_ShouldReturnValueAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNotNull()
    {
	    //Arrange
	    var value = Helpers.ExampleIntValue;
	    var nameStack = new NameStack("");
	    nameStack.PushProperty(Helpers.PropertyName);
	    var parameters = new OStructParameter(value);
	    var property = new OptionalStructProperty<OStructParameter, int>(parameters, nameStack);
        
	    //Act
	    var validatedProperty = property.Map(p => p.Value);
        
	    //Assert
	    validatedProperty.ShouldBe(value);
	    
	    property.ValidationResult.HasFailed.ShouldBeFalse();
	    
	    nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
    }
        
	[Fact] 
	public void DirectMap_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructParameter(null);
		var property = new OptionalStructProperty<OStructParameter, int>(parameters, nameStack);
        
		//Act
		var validatedProperty = property.Map(p => p.Value);
        
		//Assert
		validatedProperty.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
}