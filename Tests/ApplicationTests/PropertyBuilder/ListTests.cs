using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.PropertyBuilder;

public class ListTests
{
    [Fact]
    public void ListProperty_ShouldReturnListProperty_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.ListProperty(x => x.RequiredListProperty);
    
        //Assert
        builder.ShouldBeOfType<ListProperty<Parameters, int>>();
    }
    
    [Fact]
    public void ListProperty_ShouldAddPropertyToList_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.ListProperty(x => x.RequiredListProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void ListProperty_ShouldPushPropertyNameToNameStack_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty(x => x.RequiredListProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredListProperty"));
    }
    
    [Fact]
    public void ListProperty_ShouldPushPropertyNameToNameStack_OnCastedListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty(x => (List<int>) x.RequiredListProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredListProperty"));
    }
    
    [Fact]
    public void ListProperty_ShouldThrowInvalidOperationException_OnSelfReferencingListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act & Assert
        Should.Throw<InvalidOperationException>(() => propertyBuilder.ClassProperty(x => x))
            .Message.ShouldBe(Helpers.SelfReferencingErrorMessage);
    }
    
    [Fact]
    public void ListProperty_ShouldThrowInvalidOperationException_OnCastedSelfReferencingListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act & Assert
        Should.Throw<InvalidOperationException>(() => propertyBuilder.ClassProperty(x => (Result) x))
            .Message.ShouldBe(Helpers.SelfReferencingErrorMessage);
    }
    
    [Fact]
    public void ListProperty_ShouldPushCustomNameToNameStack_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty(x => x.RequiredListProperty, Helpers.PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
    
    [Fact]
    public void ListProperty_ShouldPushCustomNameToNameStack_OnManualTypedListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NameStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty<int>(Helpers.PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(Helpers.PropertyName));
    }
}