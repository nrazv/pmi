using Microsoft.AspNetCore.Mvc;
using pmi.Tool.Models;
using pmi.Tool.Services;

namespace pmi.Tool;

[Controller]
[Route("ws")]

public class WebSocketController : Controller
{
    private readonly IWebSocketService webSocketService;
    public WebSocketController(IWebSocketService webSocketService)
    {
        this.webSocketService = webSocketService;
    }


    [HttpGet]
    public async void Get()
    {
        var context = ControllerContext.HttpContext;

        if (context.WebSockets.IsWebSocketRequest)
        {
            await webSocketService.ExecuteToolViaWebSocket(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("toolResult")]
    public async Task GetResult()
    {
        var context = ControllerContext.HttpContext;

        if (context.WebSockets.IsWebSocketRequest)
        {
            await webSocketService.GetExecutedToolResult(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

}