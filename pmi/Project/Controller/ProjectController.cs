﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;
using pmi.Project.Services;

namespace pmi.Project.Controller;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost("new", Name = "New Project")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult NewProject(string projectName)
    {
        var (project, errorMessage) = _projectService.NewProject(projectName);
        if (errorMessage is null)
        {
            return Ok(project);
        }

        return BadRequest(errorMessage);
    }

    [HttpGet("all", Name = "Get all projects")]
    public List<ProjectDto> GetAll()
    {
        return _projectService.GetProjects();
    }

}
