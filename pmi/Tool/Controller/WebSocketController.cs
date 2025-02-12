using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using pmi.Tool.Models;

namespace pmi.Tool;

[Controller]
[Route("ws")]

public class WebSocketController : Controller
{
    private AsyncToolService _toolService;
    public WebSocketController(AsyncToolService toolService)
    {
        _toolService = toolService;
    }


    [HttpGet]
    public async Task Get()
    {
        var context = ControllerContext.HttpContext;

        if (context.WebSockets.IsWebSocketRequest)
        {
            using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
            {
                await _toolService.ExecuteToolAsync(context, webSocket);
            }
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}