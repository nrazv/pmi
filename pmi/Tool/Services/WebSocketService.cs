using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using pmi.Tool.Mappers;
using pmi.Tool.Managers;

namespace pmi.Tool.Services;

public class WebSocketService : IWebSocketService
{
    ILogger<Program> logger;
    private readonly ToolWSMapper toolWSMapper;
    private readonly ProcessManager processManager = null!;
    private readonly ObservableProcessResults processResults;



    public WebSocketService(ObservableProcessResults processResults, ILogger<Program> logger)
    {
        this.logger = logger;
        toolWSMapper = new ToolWSMapper();
        this.processResults = processResults;
    }

    public async Task GetExecutedToolResult(HttpContext context)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var buffer = new byte[1024 * 4];
        string? listeningKey = null;

        // Subscribe to result updates
        void OnResultChanged(object? sender, ProcessResultChangedEventArgs e)
        {
            if (e.Key == listeningKey)
            {
                var message = Encoding.UTF8.GetBytes(e.NewValue ?? "");
                webSocket.SendAsync(
                    new ArraySegment<byte>(message, 0, message.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
        }

        processResults.ResultChanged += OnResultChanged;
        try
        {
            // Wait for client to send the key
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            listeningKey = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Client is now listening to: {listeningKey}");

            // Keep socket open while client is connected
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        finally
        {
            processResults.ResultChanged -= OnResultChanged;
            webSocket.Dispose();
        }

    }


}