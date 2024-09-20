﻿//using FluentResults;
//using FluentValidation;
//using FluentValidation.Results;
//using MediatR;
//using Newtonsoft.Json;

//namespace Core.Common.Middlewares;

//public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : class, IRequest<TResponse>
//    where TResponse : class
//{
//    private readonly bool _hasValidators;
//    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
//    private readonly IEnumerable<IValidator<TRequest>> _validators;

//    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
//    {
//        _validators = validators;
//        _logger = logger;
//        _hasValidators = _validators.Any();
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        _logger.LogTrace("Handling {RequestType}", typeof(TRequest).Name);

//        if (!_hasValidators)
//        {
//            _logger.LogTrace("No validators configured for {RequestType}", typeof(TRequest).Name);
//            return await next();
//        }

//        var context = new ValidationContext<TRequest>(request);
//        ValidationResult[] validationResults =
//                    await Task.WhenAll(_validators.Select(v =>
//                           v.ValidateAsync(context, cancellationToken)));

//        List<ValidationFailure> failures =
//                    validationResults.SelectMany(r => r.Errors)
//                          .Where(f => f != null).ToList();

//        if (!failures.Any())
//        {
//            _logger.LogTrace("Validation passed for {RequestType}", typeof(TRequest).Name);
//            return await next();
//        }

//        var errorJson = JsonConvert.SerializeObject(failures);
//        _logger.LogWarning("Validation failed for {RequestType}: {Errors}", typeof(TRequest).Name, errorJson);

//        if (IsResultType(typeof(TResponse)))
//            return (dynamic)Result.Fail(errorJson);

//        throw new ValidationException(failures);
//    }

//    private static bool IsResultType(Type responseType)
//    {
//        return responseType == typeof(Result) ||
//               responseType.IsGenericType
//                && responseType.GetGenericTypeDefinition() == typeof(Result<>);
//    }
//}