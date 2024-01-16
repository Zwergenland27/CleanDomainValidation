// See https://aka.ms/new-console-template for more information
using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;

Params request = new("TIME");

var command = CommandBuilder<Rekord>
	.AddParameter(request)
	.ValidateByUsing<Tests>();

if (command.HasFailed)
{
	Console.Write(command.Errors[0].Code);
	return;
}

Console.WriteLine(command.Value);

internal class Tests : CommandValidator<Params, Rekord>
{
	public override void Configure(Params parameter)
	{
		Type t = RequiredEnum<Type>(parameter.Type, Error.Validation("1", "2"), Error.Validation("3", "4"));

		CreateInstance(() => new Rekord(t));
	}
}

public record Params(string? Type) : IParameter;

public record Rekord(Type t) : ICommand;


public enum Type
{
	SMS,
	TIME,
	Wildschwein
}