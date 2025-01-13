using AutoMapper;
using pmi.Project.Models;

namespace pmi.Project.Services;

public class ProjectManager
{
    private readonly string _projectFolder;
    IMapper _mapper;

    public ProjectManager(string projectFolder)
    {
        _projectFolder = projectFolder;
        _mapper = new Mapper(new MapperConfiguration(conf =>
        {
            conf.CreateMap<DirectoryInfo, ProjectEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetHashCode()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }));

    }

    public ProjectEntity? createNewProject(string projectName, out string? errorMessage)
    {
        string newProjectPath = $@"{_projectFolder}/{projectName}";
        errorMessage = null;


        if (Directory.Exists(newProjectPath))
        {
            errorMessage = "Project already exists";
            return null;
        }

        DirectoryInfo newProjectFolder = Directory.CreateDirectory(newProjectPath);

        return _mapper.Map<ProjectEntity>(newProjectFolder);
    }

    public List<ProjectEntity> GetProjects()
    {
        string[] projectsDirectory;
        var projectsList = new List<ProjectEntity>();
        try
        {
            projectsDirectory = Directory.GetDirectories(_projectFolder);
        }
        catch (DirectoryNotFoundException)
        {
            return projectsList;
        }


        foreach (string project in projectsDirectory)
        {
            string projectName = project.Replace($@"{_projectFolder}\", string.Empty);
            var projectEntity = GetProjectByName(projectName);
            projectsList.Add(projectEntity);
        }

        return projectsList;
    }

    private ProjectEntity GetProjectByName(string projectName)
    {
        DirectoryInfo dirInfo = new DirectoryInfo($@"{_projectFolder}\{projectName}");
        return _mapper.Map<ProjectEntity>(dirInfo);
    }
}
