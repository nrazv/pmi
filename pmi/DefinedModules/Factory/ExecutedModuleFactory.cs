using pmi.DefinedModules.Models;
using pmi.ExecutedTool.Models;
using pmi.Modules.Models;
using pmi.Project.Models;

namespace pmi.DefinedModules.Factory;

public class ExecutedModuleFactory : IExecutedModuleFactory
{
    public ExecutedModuleEntity CreateExecutedModule(ModuleEntity moduleEntity, ModuleExecutionRequest request, ProjectEntity project)
    {
        ExecutedModuleEntity NewExecutedModule = new ExecutedModuleEntity
        {
            Id = Guid.NewGuid(),
            Name = moduleEntity.Name,
            Target = request.Target,
            ProjectId = project.Id,
            Project = project,
            ExecutedTools = new List<ExecutedToolEntity>()
        };

        foreach (var executableTool in moduleEntity.ExecutablesTools)
        {
            NewExecutedModule.ExecutedTools.Add(new ExecutedToolEntity
            {
                Id = Guid.NewGuid(),
                ExecutedModuleId = NewExecutedModule.Id,
                ExecutedModule = NewExecutedModule,
                Name = executableTool.Name,
                ToolArguments = executableTool.Arguments,
                Target = request.Target,

            });
        }

        return NewExecutedModule;
    }
}