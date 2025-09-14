using Microsoft.AspNetCore.Mvc;
using pmi.Tool.Models;
using pmi.Tool.Services;

namespace pmi.Tool;

[Controller]
[Route("ws")]

public class WebSocketController : Controller
{
    private readonly IWebSocketService webSocketService;
    private IToolService _toolService;
    public WebSocketController(IWebSocketService webSocketService, IToolService toolService)
    {
        _toolService = toolService;
        this.webSocketService = webSocketService;
    }


    [HttpGet("toolOutput")]
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