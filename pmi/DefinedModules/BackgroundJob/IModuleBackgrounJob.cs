namespace pmi.DefinedModules.BackgroundJob;

public interface IModuleBackgroundJob
{
    Guid ExecutionId { get; }
    Task ExecuteAsync(CancellationToken cancellationToken);
}