namespace TaskFlow.API.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            _logger.LogInformation("Incoming Request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context);
            stopwatch.Stop();

            _logger.LogInformation("Outgoing Response: {StatusCode} ({ElapseMilliseconds}ms)", context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
    }
}
