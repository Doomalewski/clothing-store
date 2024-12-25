using clothing_store.Interfaces;
using clothing_store.Models;
using System.Security.Claims;

public class VisitLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string[] _staticFileExtensions = new[] { ".css", ".js", ".jpg", ".jpeg", ".png", ".gif", ".svg", ".ico", ".woff", ".woff2", ".ttf", ".eot" };

    public VisitLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Sprawdzamy, czy żądanie dotyczy pliku statycznego
        var requestPath = context.Request.Path.ToString();
        if (_staticFileExtensions.Any(extension => requestPath.EndsWith(extension, StringComparison.OrdinalIgnoreCase)))
        {
            // Jeśli to plik statyczny, pomijamy logowanie
            await _next(context);
            return;
        }

        // Tworzymy scope, aby uzyskać dostęp do scoped services
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userEventService = scope.ServiceProvider.GetRequiredService<IUserEventService>();

            // Zbieranie danych z HttpContext
            var userId = context.User?.Identity?.IsAuthenticated == true ? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value : "Anonymous"; // Jeśli użytkownik jest zalogowany, pobieramy jego ID, w przeciwnym razie zapisujemy "Anonymous"
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP"; // Jeśli nie uda się pobrać IP, zapisujemy "Unknown IP"
            var pageUrl = context.Request.Path; // Ścieżka URL
            var eventTime = DateTime.UtcNow; // Czas zdarzenia

            // Tworzymy obiekt UserEvent
            var userEvent = new UserEvent
            {
                EventType = "Visit", // Typ zdarzenia
                EventTime = eventTime,
                UserId = userId,
                IPAddress = ipAddress,
                PageUrl = pageUrl
            };

            // Logujemy zdarzenie
            await userEventService.LogEventAsync(userEvent);
        }

        // Przechodzimy do kolejnego middleware
        await _next(context);
    }
}
