using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using pmi.DataContext;
using pmi.Tool.Models;
using pmi.Tool.Services;

namespace pmi.Tool;

[Controller]
[Route("ws")]

public class WebSocketController : Controller
{
    private AsyncToolService _toolService;
    private readonly IWebSocketService webSocketService;
    public WebSocketController(AsyncToolService toolService, IWebSocketService webSocketService)
    {
        _toolService = toolService;
        this.webSocketService = webSocketService;
    }


    [HttpGet]
    public async Task Get()
    {
        var context = ControllerContext.HttpContext;

        if (context.WebSockets.IsWebSocketRequest)
        {
            await _toolService.ExecuteToolViaWebSocket(context);
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