
using System.Threading.Channels;
using pmi.Data;
using pmi.DataContext;
using pmi.ExecutedTool;
using pmi.ExecutedTool.Repository;
using pmi.ExecutedTool.Service;
using pmi.Project.Builders;
using pmi.Project.Repository;
using pmi.Project.Service;
using pmi.Tool.Models;
using pmi.Tool.Services;
using Microsoft.EntityFrameworkCore;
using pmi.DefinedModules.Services;
using pmi.Modules.Service;
using pmi.Modules.Repository;
using pmi.ExecutedModule.Repository;
using pmi.ExecutedModule.Service;
using pmi.DefinedModules.Factory;
using pmi.DefinedModules.BackgroundJob;
using pmi.Subdomain.Repository;
using pmi.Subdomain.Service;

namespace pmi;

public class Program
{
    public static void Main(string[] args)
    {

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:3000", "http://localhost:5000")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials();
                              });
        });


        builder.Services.AddDbContext<PmiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );


        // SignalR
        builder.Services.AddSignalR();


        // Channel for tool
        var toolsChannel = Channel.CreateBounded<ToolJob>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait
        });

        // Channel for modules
        var modulesChannel = Channel.CreateBounded<IModuleBackgroundJob>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait
        });

        builder.Services.AddSingleton(toolsChannel);
        builder.Services.AddSingleton(modulesChannel);

        // Add services
        builder.Services.AddScoped<IToolService, ToolService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IExecutedToolService, ExecutedToolService>();
        builder.Services.AddScoped<IDefinedModuleService, DefinedModuleService>();
        builder.Services.AddScoped<IModuleService, ModuleService>();
        builder.Services.AddScoped<IExecutedModuleService, ExecutedModuleService>();
        builder.Services.AddScoped<ISubdomainService, SubdomainService>();


        // Add Background services
        builder.Services.AddHostedService<ToolExecutionBackgroundService>();
        builder.Services.AddHostedService<ModuleBackgroundService>();

        // Add repository
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<IExecutedToolRepository, ExecutedToolRepository>();
        builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
        builder.Services.AddScoped<IExecutedModuleRepository, ExecutedModuleRepository>();
        builder.Services.AddScoped<ISubdomainRepository, SubdomainRepository>();

        // Add Factories
        builder.Services.AddScoped<IExecutedModuleFactory, ExecutedModuleFactory>();

        builder.Services.AddScoped<ToolsDataJSON>();
        builder.Services.AddScoped<ProjectEntityBuilder>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        var env = app.Environment.EnvironmentName;
        app.Logger.LogInformation("Running in environment: {Environment}", env);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        //To be removed
        app.UseSwagger();
        app.UseSwaggerUI();

        // Serve static files (React builds in wwwroot)
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<ToolHub>("/toolhub");
        app.UseWebSockets();
        app.Run();
    }
}
