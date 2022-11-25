using SimpleEmailApp.CorrelationService.Middleware;

namespace SimpleEmailApp.CorrelationService.Service
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
    }
}
