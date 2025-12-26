
using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;
using pmi.DefinedModules.BackgroundJob;
using pmi.DefinedModules.ModulesHub;

namespace pmi.DefinedModules.Services;

public class ModuleBackgroundService : BackgroundService
{
    private readonly Channel<IModuleBackgroundJob> _channel;
    private readonly IHubContext<DefinedModuleHub> _hub;

    public ModuleBackgroundService(Channel<IModuleBackgroundJob> channel, IHubContext<DefinedModuleHub> hub)
    {
        _channel = channel;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var reader = _channel.Reader;

        while (await reader.WaitToReadAsync(stoppingToken))
        {
            while (reader.TryRead(out var job))
            {
                _ = Task.Run(() => HandleJobAsync(job, stoppingToken), stoppingToken);
            }
        }
    }

    private async Task HandleJobAsync(IModuleBackgroundJob job, CancellationToken cancellationToken)
    {
        await job.ExecuteAsync(cancellationToken);
    }
}