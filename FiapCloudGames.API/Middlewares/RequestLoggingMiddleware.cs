namespace FiapCloudGames.Api.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Items["X-Correlation-ID"]?.ToString();
            var startTime = DateTime.UtcNow;

            _logger.LogInformation(
                "Request iniciada: {Method} {Path} | CID={CorrelationId}",
                context.Request.Method,
                context.Request.Path,
                correlationId);

            await _next(context);

            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation(
                "Request finalizada: {StatusCode} em {Milliseconds}ms | CID={CorrelationId}",
                context.Response.StatusCode,
                duration.TotalMilliseconds,
                correlationId);
        }
    }
}
