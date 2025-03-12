using System.Reflection;
using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests;

public enum TestEnum
{
    One,
    Two,
    Three,
    Four
}

public static class Helpers
{
    public static Error ExampleValidationError => Error.Validation("Error.Validation", "An error occured");
    
    public static Error ExampleMissingError => Error.Validation("Error.Missing", "The value is missing");
    
    public static string PropertyName => "PropertyName";

    public static string DefaultStringValue => "default";
    public static string DefaultStringAlternateValue => "alternate-default";
    public static string ExampleStringValue => "value";
    public static string AlternateStringValue => "alternate";
    public static string ErrorStringValue => "error";
    
    public static int DefaultIntValue => 3;
    public static int DefaultIntAlternateValue => 3;
    public static int ExampleIntValue => 1;
    public static int AlternateIntValue => 2;
    public static int ErrorIntValue => 9;
    
    public static Error ExampleInvalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");
    public static TestEnum DefaultEnumValue => TestEnum.Three;
    public static TestEnum DefaultEnumAlternateValue => TestEnum.Four;
    public static string EnumOneString => "One";
    public static string EnumTwoString => "Two";
    public static string EnumInvalidString => "Invalid";
    
    public static int EnumOneInt => 0;
    public static int EnumTwoInt => 1;
    public static int EnumInvalidInt => 9;
    
    public static void ShouldNotContainPropertyName(this NameStack nameStack, PropertyNameEntry propertyName)
    {
        var stackFieldInfo = typeof(NameStack).GetField("_propertyNamesStack", BindingFlags.NonPublic | BindingFlags.Instance);
        var propertyNameStack = (Stack<INameStackEntry>) stackFieldInfo!.GetValue(nameStack)!;
        propertyNameStack.ShouldNotContain(propertyName);
    }

    public static void ShouldPeekPropertyName(this NameStack nameStack, PropertyNameEntry propertyName)
    {
        var stackFieldInfo = typeof(NameStack).GetField("_propertyNamesStack", BindingFlags.NonPublic | BindingFlags.Instance);
        var propertyNameStack = (Stack<INameStackEntry>) stackFieldInfo!.GetValue(nameStack)!;
        propertyNameStack.Peek().ShouldBe(propertyName);
    }

    public static void NameStackShouldPeekPropertyName<T1, T2>(this PropertyBuilder<T1, T2> builder,
        PropertyNameEntry propertyName)
    where T1 : notnull
    where T2 : notnull
    {
        var nameStackFieldInfo = typeof(PropertyBuilder<T1, T2>).GetField("_nameStack", BindingFlags.NonPublic | BindingFlags.Instance);
        var propertyNameStack = (NameStack) nameStackFieldInfo!.GetValue(builder)!;
        propertyNameStack.ShouldPeekPropertyName(propertyName);
    }
}