using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MediatR;

using FinancialDocument.Application.Configurations;

namespace FinancialDocument.Application.Behaviours;
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly PerformanceThresholdSettings _performanceThresholdSettings;

    public PerformanceBehaviour(ILogger<TRequest> logger, IOptions<PerformanceThresholdSettings> options)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _performanceThresholdSettings = options.Value;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > _performanceThresholdSettings.BoundaryMiliseconds)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning(
                "FinancialDocument Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName,
                elapsedMilliseconds,
                request);
        }

        return response;
    }
}
