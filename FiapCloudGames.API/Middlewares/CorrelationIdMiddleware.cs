using FiapCloudGames.Domain.Extensions;

namespace FiapCloudGames.Api.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.Headers[HeaderName].FirstOrDefault();

            if (correlationId.IsNullOrEmpty())
                correlationId = Guid.NewGuid().ToString();

            context.Items[HeaderName] = correlationId;

            context.Response.Headers[HeaderName] = correlationId;

            await _next(context);
        }
    }
}
