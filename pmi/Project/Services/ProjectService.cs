using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;

namespace pmi.Project.Services;

public class ProjectService : IProjectService
{
    private readonly string _rootPath;

    public ProjectService(IConfiguration configuration)
    {
        _rootPath = configuration.GetSection("RootFolder").Value ?? "projects";
    }
    public ProjectEntity GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public (ProjectEntity?, string? errorMessage) NewProject(string projectName)
    {
        var project = createNewProject(projectName, out string errorMessage);

        return (project, errorMessage);
    }


    private ProjectEntity? createNewProject(string projectname, out string errorMessage)
    {
        string newProjectPath = $@"{_rootPath}/{projectname}";
        errorMessage = null;

        if (Directory.Exists(newProjectPath))
        {
            errorMessage = "Project already exists";
            return null;
        }

        DirectoryInfo newProjectFolder = Directory.CreateDirectory(newProjectPath);
        var createdDate = newProjectFolder.CreationTime;
        var lastUpdate = newProjectFolder.LastWriteTime;
        var name = newProjectFolder.Name;

        return new ProjectEntity(id: "awdaw23", name, createdDate, lastUpdate);
    }
}
