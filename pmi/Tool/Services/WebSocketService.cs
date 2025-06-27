using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using pmi.Tool.Mappers;
using pmi.Tool.Models;

namespace pmi.Tool.Services;

public class WebSocketService : IWebSocketService
{
    private readonly Dictionary<string, Dictionary<string, WebSocket>> _projectClients;
    private readonly ToolWSMapper _toolWSMapper;


    public WebSocketService()
    {
        _projectClients = new Dictionary<string, Dictionary<string, WebSocket>>();
        _toolWSMapper = new ToolWSMapper();
    }


    public void RegisterClient(WebSocket client, ToolExecutionRequest request)
    {
        if (_projectClients.ContainsKey(request.ProjectName))
        {
            var projectClients = _projectClients[request.ProjectName];
            if (request.ClientId is not null && !projectClients.ContainsKey(request.ClientId))
            {
                projectClients.Add(request.ClientId, client);
            }
        }
        else
        {
            var clients = new Dictionary<string, WebSocket>();
            clients.Add(request.ClientId!, client);
            _projectClients.Add(request.ProjectName, clients);
        }
    }

    public void UnregisterClient(ToolExecutionRequest request)
    {
        if (_projectClients.ContainsKey(request.ProjectName))
        {
            var projectClients = _projectClients[request.ProjectName];
            if (projectClients.ContainsKey(request.ClientId))
            {
                projectClients.Remove(request.ClientId);
            }
        }
    }


}