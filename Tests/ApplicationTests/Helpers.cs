using System.Reflection;
using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests;

public static class Helpers
{
    public static Error ExampleValidationError => Error.Validation("Error.Validation", "An error occured");
    
    public static Error ExampleMissingError => Error.Validation("Error.Missing", "The value is missing");
    
    public static string PropertyName => "PropertyName";

    public static string DefaultStringValue => "default";
    public static string ExampleStringValue => "value";
    public static string ErrorStringValue => "error";
    
    public static int DefaultIntValue => 3;
    public static int ExampleIntValue => 1;
    public static int ErrorIntValue => 9;
    
    public static void ShouldNotContainPropertyName(this NamingStack nameStack, PropertyNameEntry propertyName)
    {
        var stackFieldInfo = typeof(NamingStack).GetField("_propertyNamesStack", BindingFlags.NonPublic | BindingFlags.Instance);
        var propertyNameStack = (Stack<INameStackEntry>) stackFieldInfo!.GetValue(nameStack)!;
        propertyNameStack.ShouldNotContain(propertyName);
    }
}