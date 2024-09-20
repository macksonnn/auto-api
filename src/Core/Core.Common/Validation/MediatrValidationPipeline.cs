using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Core.Common.Validation;

/// <summary>
/// This class is responsible for validate the Commands and Queries before executing the Handler
/// </summary>
/// <typeparam name="TRequest">The IRequest object to be validated</typeparam>
/// <typeparam name="TResponse">The expected result of the operation with the generic type</typeparam>
public class MediatrValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly bool _hasValidators;
    private readonly ILogger<MediatrValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public MediatrValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<MediatrValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
        _hasValidators = _validators.Any();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_hasValidators)
        {
            _logger.LogTrace("No validators configured for {RequestType}", typeof(TRequest).Name);
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults =
                    await Task.WhenAll(_validators.Select(v =>
                           v.ValidateAsync(context, cancellationToken)));

        List<ValidationFailure> failures =
                    validationResults.SelectMany(r => r.Errors)
                          .Where(f => f != null).ToList();

        if (failures.Any())
        {
            _logger.LogTrace($"Validation failed for {typeof(TRequest).Name}");

            var result = new TResponse();

            var groupedFailures = failures
            .GroupBy(f => f.PropertyName)
            .Select(g => new Error(g.Key).WithMetadata(g.ToDictionary(f => f.ErrorMessage, z => z.PropertyName as object)));

            result.Reasons.AddRange(groupedFailures);

            return result;
        }

        _logger.LogTrace("Validation passed for {RequestType}", typeof(TRequest).Name);
        var success = await next();

        return success;
    }

    private static bool IsResultType(Type responseType)
    {
        return responseType == typeof(Result) ||
               (responseType.IsGenericType
                && responseType.GetGenericTypeDefinition() == typeof(Result<>));
    }
}