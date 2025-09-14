namespace pmi.Tool.Models;

using Microsoft.AspNetCore.SignalR;

public class ToolHub : Hub
{
    public Task JoinToolGroup(string executionId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, executionId);
    }

    public Task LeaveToolGroup(string executionId)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, executionId);
    }
}
