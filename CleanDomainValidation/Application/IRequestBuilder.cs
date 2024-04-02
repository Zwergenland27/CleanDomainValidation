namespace CleanDomainValidation.Application;

public interface IRequestBuilder<TParameters, TRequest>
	where TParameters : IParameters
	where TRequest : IRequest

{
	ValidatedRequiredProperty<TRequest> Configure(RequiredPropertyBuilder<TParameters, TRequest> builder);
}
