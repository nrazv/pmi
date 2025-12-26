using Microsoft.AspNetCore.SignalR;

namespace pmi.DefinedModules.ModulesHub;

public class DefinedModuleHub : Hub
{
    public Task JoinModuleGroup(string executionId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, executionId);
    }
}