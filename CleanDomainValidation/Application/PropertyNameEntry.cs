namespace CleanDomainValidation.Application;

public interface INameStackEntry;
public record PropertyNameEntry(string Name) : INameStackEntry;

public record IndexEntry(int Index) : INameStackEntry;